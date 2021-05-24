using System.Net.Sockets;
using System.Text;

namespace MdServices.Base
{
    /// <summary>
    /// WebSocket session
    /// </summary>
    /// <remarks> WebSocket session is used to read and write data from the connected WebSocket client. Thread-safe.</remarks>
    public class WsSession : HttpSession, IWebSocket
    {
        internal readonly WebSocket WebSocket;

        public WsSession(WsServer server) : base(server) 
        {
            Context<WsSession>.Logger.Trace("-> WsSession::WsSession");
            WebSocket = new WebSocket(this);
            Context<WsSession>.Logger.Trace("<- WsSession::WsSession");
        }

        // WebSocket connection methods
        public virtual bool Close(int status) 
        {
            Context<WsSession>.Logger.Trace("-> WsSession::Close");
            SendCloseAsync(status, null, 0, 0); base.Disconnect(); return true;
            Context<WsSession>.Logger.Trace("<- WsSession::Close");
        }

        #region WebSocket send text methods

        public long SendText(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendText");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_TEXT, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendText");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendText");
        }

        public long SendText(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendText");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_TEXT, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendText");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendText");
        }

        public bool SendTextAsync(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendTextAsync");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_TEXT, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendTextAsnyc");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendTextAsync");
        }

        public bool SendTextAsync(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendTextAsync");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_TEXT, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendTextAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendTextAsync");
        }

        #endregion

        #region WebSocket send binary methods

        public long SendBinary(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendBinary");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_BINARY, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendBinary");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendBinary");
        }

        public long SendBinary(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendBinary");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_BINARY, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendBinary");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendBinary");
        }

        public bool SendBinaryAsync(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendBinaryAsync");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_BINARY, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendBinaryAsnyc");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendBinaryAsnyc");
        }

        public bool SendBinaryAsync(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendBinaryAsync");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_BINARY, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendBinaryAsnyc");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendBinaryAsnyc");
        }

        #endregion

        #region WebSocket send close methods

        public long SendClose(int status, byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendClose");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_CLOSE, false, buffer, offset, size, status);
                Context<WsSession>.Logger.Trace("<- WsSession::SendClose");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendClose");
        }

        public long SendClose(int status, string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendClose");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_CLOSE, false, data, 0, data.Length, status);
                Context<WsSession>.Logger.Trace("<- WsSession::SendClose");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendClose");
        }

        public bool SendCloseAsync(int status, byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendCloseAsync");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_CLOSE, false, buffer, offset, size, status);
                Context<WsSession>.Logger.Trace("<- WsSession::SendCloseAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendCloseAsync");
        }

        public bool SendCloseAsync(int status, string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendCloseAsync");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_CLOSE, false, data, 0, data.Length, status);
                Context<WsSession>.Logger.Trace("<- WsSession::SendCloseAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendCloseAsync");
        }

        #endregion

        #region WebSocket send ping methods

        public long SendPing(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPing");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PING, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPing");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPing");
        }

        public long SendPing(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPing");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PING, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPing");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPing");
        }

        public bool SendPingAsync(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPingAsync");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PING, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPingAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPingAsync");
        }

        public bool SendPingAsync(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPingAsync");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PING, true, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPingAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPingAsync");
        }

        #endregion

        #region WebSocket send pong methods

        public long SendPong(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPong");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PONG, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPong");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPong");
        }

        public long SendPong(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPong");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PONG, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPong");
                return base.Send(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPong");
        }

        public bool SendPongAsync(byte[] buffer, long offset, long size)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPongAsync");
            lock (WebSocket.WsSendLock)
            {
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PONG, false, buffer, offset, size);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPongAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPongAsync");
        }

        public bool SendPongAsync(string text)
        {
            Context<WsSession>.Logger.Trace("-> WsSession::SendPongAsync");
            lock (WebSocket.WsSendLock)
            {
                var data = Encoding.UTF8.GetBytes(text);
                WebSocket.PrepareSendFrame(WebSocket.WS_FIN | WebSocket.WS_PONG, false, data, 0, data.Length);
                Context<WsSession>.Logger.Trace("<- WsSession::SendPongAsync");
                return base.SendAsync(WebSocket.WsSendBuffer.ToArray());
            }
            Context<WsSession>.Logger.Trace("<- WsSession::SendPongAsync");
        }

        #endregion

        #region WebSocket receive methods

        public string ReceiveText()
        {
            Buffer result = new Buffer();

            if (!WebSocket.WsHandshaked)
                return result.ExtractString(0, result.Data.Length);

            Buffer cache = new Buffer();

            // Receive WebSocket frame data
            while (!WebSocket.WsReceived)
            {
                int required = WebSocket.RequiredReceiveFrameSize();
                cache.Resize(required);
                int received = (int)base.Receive(cache.Data, 0, required);
                if (received != required)
                    return result.ExtractString(0, result.Data.Length);
                WebSocket.PrepareReceiveFrame(cache.Data, 0, received);
            }

            // Copy WebSocket frame data
            result.Append(WebSocket.WsReceiveBuffer.ToArray(), WebSocket.WsHeaderSize, WebSocket.WsHeaderSize + WebSocket.WsPayloadSize);
            WebSocket.PrepareReceiveFrame(null, 0, 0);
            return result.ExtractString(0, result.Data.Length);
        }

        public Buffer ReceiveBinary()
        {
            Buffer result = new Buffer();

            if (!WebSocket.WsHandshaked)
                return result;

            Buffer cache = new Buffer();

            // Receive WebSocket frame data
            while (!WebSocket.WsReceived)
            {
                int required = WebSocket.RequiredReceiveFrameSize();
                cache.Resize(required);
                int received = (int)base.Receive(cache.Data, 0, required);
                if (received != required)
                    return result;
                WebSocket.PrepareReceiveFrame(cache.Data, 0, received);
            }

            // Copy WebSocket frame data
            result.Append(WebSocket.WsReceiveBuffer.ToArray(), WebSocket.WsHeaderSize, WebSocket.WsHeaderSize + WebSocket.WsPayloadSize);
            WebSocket.PrepareReceiveFrame(null, 0, 0);
            return result;
        }

        #endregion

        #region Session handlers

        protected override void OnDisconnecting()
        {
            if (WebSocket.WsHandshaked)
                OnWsDisconnecting();
        }

        protected override void OnDisconnected()
        {
            // Disconnect WebSocket
            if (WebSocket.WsHandshaked)
            {
                WebSocket.WsHandshaked = false;
                OnWsDisconnected();
            }

            // Reset WebSocket upgrade HTTP request and response
            Request.Clear();
            Response.Clear();

            // Clear WebSocket send/receive buffers
            WebSocket.ClearWsBuffers();

            // Initialize new WebSocket random nonce
            WebSocket.InitWsNonce();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            // Check for WebSocket handshaked status
            if (WebSocket.WsHandshaked)
            {
                // Prepare receive frame
                WebSocket.PrepareReceiveFrame(buffer, offset, size);
                return;
            }

            base.OnReceived(buffer, offset, size);
        }

        protected override void OnReceivedRequestHeader(HttpRequest request)
        {
            // Check for WebSocket handshaked status
            if (WebSocket.WsHandshaked)
                return;

            // Try to perform WebSocket upgrade
            if (!WebSocket.PerformServerUpgrade(request, Response))
            {
                base.OnReceivedRequestHeader(request);
                return;
            }
        }

        protected override void OnReceivedRequest(HttpRequest request)
        {
            // Check for WebSocket handshaked status
            if (WebSocket.WsHandshaked)
            {
                // Prepare receive frame from the remaining request body
                var body = Request.Body;
                var data = Encoding.UTF8.GetBytes(body);
                WebSocket.PrepareReceiveFrame(data, 0, data.Length);
                return;
            }

            base.OnReceivedRequest(request);
        }

        protected override void OnReceivedRequestError(HttpRequest request, string error)
        {
            // Check for WebSocket handshaked status
            if (WebSocket.WsHandshaked)
            {
                OnError(new SocketError());
                return;
            }

            base.OnReceivedRequestError(request, error);
        }

        #endregion

        #region Web socket handlers

        public virtual void OnWsConnecting(HttpRequest request) {}
        public virtual void OnWsConnected(HttpResponse response) {}
        public virtual bool OnWsConnecting(HttpRequest request, HttpResponse response) { return true; }
        public virtual void OnWsConnected(HttpRequest request) {}
        public virtual void OnWsDisconnecting() {}
        public virtual void OnWsDisconnected() {}
        public virtual void OnWsReceived(byte[] buffer, long offset, long size) {}
        public virtual void OnWsClose(byte[] buffer, long offset, long size) { Close(1000); }
        public virtual void OnWsPing(byte[] buffer, long offset, long size) { SendPongAsync(buffer, offset, size); }
        public virtual void OnWsPong(byte[] buffer, long offset, long size) {}
        public virtual void OnWsError(string error) { OnError(SocketError.SocketError); }
        public virtual void OnWsError(SocketError error) { OnError(error); }

        public void SendUpgrade(HttpResponse response) { SendResponseAsync(response); }

        #endregion
    }
}
