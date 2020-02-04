using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi_ServerHost
{
    class HostHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _serverName = _packet.ReadString();

            Console.WriteLine($"{Host.servers[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now server {_fromClient}");
            if (_fromClient != _clientIdCheck) // Something went terribly wrong
            {
                Console.WriteLine($"server \"{_serverName}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck}).");
            }
        }
    }
}
