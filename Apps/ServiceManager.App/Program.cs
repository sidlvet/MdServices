using System;
using System.IO;
using System.Reflection;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using MdServices.Base;

namespace MdServices
{
    class Program
    {
        static void Main(string[] args)
        {

            // WebSocket server port
            int port = 8443;
            if (args.Length > 0)
                port = int.Parse(args[0]);
            // WebSocket server content path
            string www = "./wss";
            if (args.Length > 1)
                www = args[1];

            Context<Program>.Logger.Info($"WebSocket server port: {port}");
            Context<Program>.Logger.Info($"WebSocket server static content path: {www}");
            Context<Program>.Logger.Info($"WebSocket server website: https://h2841676.stratoserver.net:{port}/chat/index.html");

            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Context<Program>.Logger.Info("Directory: " + dir);

            // Create and prepare a new SSL server context
            var context = new SslContext(SslProtocols.Tls12, new X509Certificate2(dir + "/server.pfx", "qwerty"));

            // Create a new WebSocket server
            var server = new ChatServer(context, IPAddress.Any, port);
            server.AddStaticContent(www, "/chat");

            // Start the server
            Context<Program>.Logger.Info("Server starting...");
            server.Start();
            Context<Program>.Logger.Info("Done!");

            Context<Program>.Logger.Info("Press Enter to stop the server or '!' to restart the server...");

            // Perform text input
            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Context<Program>.Logger.Info("Server restarting...");
                    server.Restart();
                    Context<Program>.Logger.Info("Done!");
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                server.MulticastText(line);
            }

            // Stop the server
            Context<Program>.Logger.Info("Server stopping...");
            server.Stop();
            Context<Program>.Logger.Info("Done!");
        }
    }
}
