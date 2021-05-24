using System;
using System.Net;
using System.Net.Sockets;
using MdServices.Base;

namespace MdServices
{
    class ChatServer : WssServer
    {
        public ChatServer(SslContext context, IPAddress address, int port) : base(context, address, port)
        {
            Context<ChatServer>.Logger.Debug("-> ChatServer::ChatServer");
            Context<ChatServer>.Logger.Debug("<- ChatServer::ChatServer");
        }
    
        protected override SslSession CreateSession() { return new ChatSession(this); }

        protected override void OnError(SocketError error)
        {
            Context<ChatServer>.Logger.Error($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
