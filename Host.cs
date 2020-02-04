using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Shogi_ServerHost
{
    class Host
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        private static TcpListener tcpListener;
        public static Dictionary<int, Server> servers = new Dictionary<int, Server>();
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        public static void Start(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting Host...");
            InitializeServerData();

            tcpListener = new TcpListener(localAddr, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Host started on {Port}.");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _server = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            Console.WriteLine($"Incoming connection from { _server.Client.RemoteEndPoint}... ");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (servers[i].tcp.socket == null)
                {
                    servers[i].tcp.Connect(_server);
                    return;
                }
            }    

            Console.WriteLine($"{ _server.Client.RemoteEndPoint} failed to connect: Server is full or game has not finished yet.");
        }

        public static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                servers.Add(i, new Server(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)HostPackets.instance, HostHandle.WelcomeReceived },
            };

            Console.WriteLine("Initialized packets.");
        }
    }
}
