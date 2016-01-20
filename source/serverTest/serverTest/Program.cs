using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace serverTest {
    class Program {

        public const int X = 0;
        public const int Y = 0;
        public const ConsoleColor backgroundColor = ConsoleColor.Black;
        public const ConsoleColor foregroundColor = ConsoleColor.DarkGray;
        public const int width = 60;
        public const int height = 30;

        static ServerObject server; // сервер
        static Thread listenThread; // поток для прослушивания

        static void Main(string[] args) {
            try {
                server = new ServerObject();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
