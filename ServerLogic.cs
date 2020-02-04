using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi_ServerHost
{
    class ServerLogic
    {
        public static void Update()
        {
            ThreadManager.UpdateMain();
        }
    }
}
