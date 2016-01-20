using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace serverTest {
    class Package {

        public Package(string message, IPEndPoint ip) {
            this.message = message;
            this.ip = ip;
        }

        private string message;
        public string Message { get { return message; } set { message = value; } }

        private IPEndPoint ip;
        public IPEndPoint Ip { get { return ip; } set { ip = value; } }
    }
}
