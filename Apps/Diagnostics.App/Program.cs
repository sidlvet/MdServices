using System;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using MdServices.Base;

namespace WssChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // WebSocket server address
            string address = "h2841676.stratoserver.net";
            if (args.Length > 0)
                address = args[0];

            // WebSocket server port
            int port = 8443;
            if (args.Length > 1)
                port = int.Parse(args[1]);

            Context<Program>.Logger.Info($"WebSocket server address: {address}");
            Context<Program>.Logger.Info($"WebSocket server port: {port}");

            Context<Program>.Logger.Info("");

            // Create and prepare a new SSL client context
            var context = new SslContext(SslProtocols.Tls12, new X509Certificate2("client.pfx", "qwerty"), (sender, certificate, chain, sslPolicyErrors) => true);

            // Create a new TCP chat client
            var client = new DiagnosticsClient(context, address, port);

            // Connect the client
            Context<Program>.Logger.Info("Client connecting...");
            client.ConnectAsync();
            Context<Program>.Logger.Info("Done!");

            Context<Program>.Logger.Info("Press Enter to stop the client or '!' to reconnect the client...");

            // Perform text input
            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Reconnect the client
                if (line == "!")
                {
                    Context<Program>.Logger.Info("Client reconnecting...");
                    if (client.IsConnected)
                        client.ReconnectAsync();
                    else
                        client.ConnectAsync();
                    Context<Program>.Logger.Info("Done!");
                    continue;
                }

                // Send the entered text to the chat server
                client.SendTextAsync(line);
            }

            // Disconnect the client
            Context<Program>.Logger.Info("Client disconnecting...");
            client.DisconnectAndStop();
            Context<Program>.Logger.Info("Done!");
        }
    }
}