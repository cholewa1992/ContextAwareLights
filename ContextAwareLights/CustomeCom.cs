using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Ocon.Helpers;
using Ocon.Messages;
using Ocon.OconCommunication;
using Ocon.OconSerializer;

namespace ContextAwareLights
{
    public class CustomeCom : IOconComClient
    {
        private readonly IOconSerializer _serializer;
        public const int CommunicationPort = 2027;

        private readonly IPAddress _multicastAddress = IPAddress.Parse("239.5.6.7");
        public const int MulticastPort = 2025;

        private UdpClient _udpListener;
        private UdpClient _udpClient;


        public event RecievedEventHandler RecievedMessageEvent;
        public IOconPeer Address { get; private set; }

        public CustomeCom(IOconSerializer serializer)
        {
            _serializer = serializer;
            Address = new Peer(Guid.NewGuid());
            Listen();
            BroadcastListen();
        }

        public async void Listen()
        {
            var ipep = new IPEndPoint(IPAddress.Any, CommunicationPort);
            _udpListener = new UdpClient(ipep);

            while (true)
            {
                var recieved = await _udpListener.ReceiveAsync();
                var msg = _serializer.Deserialize<Message>(recieved.Buffer.GetString());
                if (RecievedMessageEvent != null)
                    RecievedMessageEvent(msg.Msg, null);
            }
        }

        public async void BroadcastListen()
        {
            _udpClient = new UdpClient();

            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpClient.ExclusiveAddressUse = false;
            _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, MulticastPort));
            _udpClient.JoinMulticastGroup(_multicastAddress);
 
            while (true)
            {
                var recieved = await _udpClient.ReceiveAsync();
                var msg = _serializer.Deserialize<Message>(recieved.Buffer.GetString());
                if(RecievedMessageEvent != null)
                    RecievedMessageEvent(msg.Msg, msg.Peer);
            }
        }


        public void Send(IOconMessage msg, IOconPeer reciever)
        {
            if (RecievedMessageEvent != null)
                RecievedMessageEvent(msg, Address);
        }

        public void Broadcast(IOconMessage msg)
        {
            foreach (
                var localIp in
                    Dns.GetHostAddresses(Dns.GetHostName())
                        .Where(i => i.AddressFamily == AddressFamily.InterNetwork))
            {
                using ( var client = new UdpClient())

                {
                    client.MulticastLoopback = true;
                    client.JoinMulticastGroup(_multicastAddress);
                    var bytes = _serializer.Serialize(new Message(msg, Address)).GetBytes();
                    client.Send(bytes, bytes.Length, new IPEndPoint(_multicastAddress, MulticastPort));
}
            }
        }

        #region Helper classes
        private class Message
        {
            public IOconMessage Msg { get; private set; }

            public IOconPeer Peer { get; private set; }

            public  Message(IOconMessage msg, IOconPeer peer)
            {
                Peer = peer;
                Msg = msg;
            }
        }

        #endregion

        public void Dispose()
        {
            _udpListener.Close();
            _udpClient.Close();
        }
    }
}
