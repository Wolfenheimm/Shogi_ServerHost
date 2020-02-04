using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi_ServerHost
{
    class HostSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Host.servers[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Host.MaxPlayers; i++)
            {
                Host.servers[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Host.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Host.servers[i].tcp.SendData(_packet);
                }
            }
        }

        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)HostPackets.instance))
            {
                _packet.Write(_toClient);
                _packet.Write(_msg);

                SendTCPData(_toClient, _packet);
            }
        }
    }
}
