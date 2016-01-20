using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace serverTest {
    class ClientObject {

        public ClientObject(IPEndPoint ip, Player player) {
            this.ip = ip;
            this.player = player;
            this.connected = true;
        }

        private IPEndPoint ip;
        public IPEndPoint IP { get { return ip; } set { ip = value; } }

        private Player player;
        public Player Player { get { return player; } set { player = value; } }

        private bool connected;
        public bool Connected { get { return connected; } set { connected = value; } }        
    }
}
