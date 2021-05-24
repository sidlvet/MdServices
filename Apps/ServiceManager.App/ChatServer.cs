using System;
using System.Net;
using System.Net.Sockets;
using MdServices.Base;

namespace MdServices
{
    class ChatServer : WssServer
    {
        public ChatServer(SslContext context, IPAddress address, int port) : base(context, address, port) { }

        protected override SslSession CreateSession() { return new ChatSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
