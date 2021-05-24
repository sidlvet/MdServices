using System;
using System.IO;
using System.Reflection;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using MdServices.Base;
using MdServices.Base.Interfaces;

namespace MdServices
{
    class Program
    {
        static ILogger _logger;
        static ILoggerFactory _loggerFactory;

        static void Main(string[] args)
        {
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddConsole(ILogger.LogLevel.Trace);
            _logger = _loggerFactory.CreateLogger<Program>();

            // WebSocket server port
            int port = 8443;
            if (args.Length > 0)
                port = int.Parse(args[0]);
            // WebSocket server content path
            string www = "./wss";
            if (args.Length > 1)
                www = args[1];

            _logger.LogInformation($"WebSocket server port: {port}");
            _logger.LogInformation($"WebSocket server static content path: {www}");
            _logger.LogInformation($"WebSocket server website: https://h2841676.stratoserver.net:{port}/chat/index.html");

            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _logger.LogInformation("Directory: " + dir);

            // Create and prepare a new SSL server context
            var context = new SslContext(SslProtocols.Tls12, new X509Certificate2(dir + "/server.pfx", "qwerty"));

            // Create a new WebSocket server
            var server = new ChatServer(context, IPAddress.Any, port);
            server.AddStaticContent(www, "/chat");

            // Start the server
            _logger.LogInformation("Server starting...");
            server.Start();
            _logger.LogInformation("Done!");

            _logger.LogInformation("Press Enter to stop the server or '!' to restart the server...");

            // Perform text input
            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    _logger.LogInformation("Server restarting...");
                    server.Restart();
                    _logger.LogInformation("Done!");
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                server.MulticastText(line);
            }

            // Stop the server
            _logger.LogInformation("Server stopping...");
            server.Stop();
            _logger.LogInformation("Done!");
        }
    }
}
