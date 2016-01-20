using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.DirectoryServices;



namespace clientTest {
    

    class Program {

        #region поля
        public const int X = 0;
        public const int Y = 0;
        public const ConsoleColor backgroundColor = ConsoleColor.Gray;
        public const ConsoleColor foregroundColor = ConsoleColor.DarkGray;
        static ConsoleColor userColor;
        public const int width = 60;
        public const int height = 30;
        public static Screen screen = null;

        public static string host;
        public static int serverPort = 8888;
        public static int localSendPort = 3031;
        public static int localListenPort = 3030;
        static UdpClient sender;
        public static Player myPlayer;

        public static volatile Dictionary<int, Player> players = new Dictionary<int, Player>();
        public static volatile Queue<string> packages = new Queue<string>();
        static volatile Queue<string> receivedPackaged = new Queue<string>();
        static List<string> serversList = new List<string>();
        #endregion

        static void Main(string[] args) {

            #region Инициализация
            screen = new Screen(new FieldWindow(width, height, new Point2D(), ' ', ConsoleColor.DarkGreen, ConsoleColor.Gray),
                                new RankingTable(18, 16, new Point2D(width + 1, 0)),
                                new PlayerInfoWindow(18, 13, new Point2D(width + 1, 17)));
            #region запрос данных от пользователя
            List<ConsoleColor> colors = new List<ConsoleColor>();
            colors.Add(ConsoleColor.Blue);
            colors.Add(ConsoleColor.DarkBlue);
            colors.Add(ConsoleColor.DarkCyan);
            colors.Add(ConsoleColor.DarkGreen);
            colors.Add(ConsoleColor.DarkRed);
            colors.Add(ConsoleColor.DarkMagenta);
            colors.Add(ConsoleColor.DarkYellow);
            colors.Add(ConsoleColor.Magenta);
            
            Console.WindowHeight = height + 2;
            Console.ResetColor();
            Console.Clear();
            
            //Console.WriteLine("Для комфортной игры, рекомендуется поставить шрифт в консоли:\n Lucida Console и размер шрифта 16");
            //Console.WriteLine("Поставили?");
            //Console.ReadKey();

            string name = "tanker";
            //while (true) {
            //    Console.Clear();
            //    Console.Write("Введите свой никнейм:");
            //    name = Console.ReadLine();
            //    if (name.Length < 12)
            //        break;
            //}

            Console.CursorVisible = false;
            ConsoleKey key;
            int cursor = 0;
            //while (true) {
            //    Console.SetCursorPosition(0, 1);
            //    Console.Write("Выберите цвет <");
            //    Console.BackgroundColor = colors[cursor];
            //    Console.Write("   ");
            //    Console.ResetColor();
            //    Console.Write(">");
            //    key = Console.ReadKey(true).Key;
            //    if (key == ConsoleKey.LeftArrow) {
            //        if (cursor > 0) {
            //            cursor--;
            //        }
            //    } else if (key == ConsoleKey.RightArrow) {
            //        if (cursor < colors.Count - 1) {
            //            cursor++;
            //        }
            //    } else if (key == ConsoleKey.Enter) {
            //        break;
            //    }
            //}

            cursor = new Random().Next(0, colors.Count);
            Console.WriteLine();
            Console.CursorVisible = true;
            
            while (true) {
                Console.WriteLine("Введите IP сервера:");
                host = Console.ReadLine();
                try {
                    IPAddress.Parse(host);
                } catch {
                    continue;
                }
                break;
            }

            localListenPort = Int32.Parse(Console.ReadLine());
            localSendPort = localListenPort + 1;
            #endregion

            Console.CursorVisible = false;
            Console.Clear();

            userColor = colors[cursor];
            myPlayer = new Player(
                name,
                new Point2D(3, 3),
                userColor,
                new Statistics());
            myPlayer.Changed = false; 

#endregion

            try {
                sender = new UdpClient(localSendPort);
                sender.Connect(IPAddress.Parse(host), serverPort);

                // запускаю новый поток для получения данных
                Thread sendThread = new Thread(new ThreadStart(SendMessage));
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                Thread handlerThread = new Thread(new ThreadStart(Handler));
                Thread tScreen = new Thread(new ThreadStart(ThreadScreen));

                receiveThread.Start();
                handlerThread.Start();
                sendThread.Start();                
                tScreen.Priority = ThreadPriority.Highest;                
                                

                // отправка первого пакета
                string message = ("0¶" + myPlayer.Name +"¶"+(int)myPlayer.Color + "¶" + localListenPort);
                packages.Enqueue(message);

                screen.updateTitle();
                tScreen.Start();

                // начинаю отправку данных
                Thread.Sleep(100);
                MainCircle();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        
        static void MainCircle()
        {
            while (true)
            {
                Thread.Sleep(10);
                myPlayer.run();
                if (myPlayer.Changed == true) {
                    string message = "1¶" + (int)myPlayer.Direction;
                    packages.Enqueue(message);
                    continue;
                }
                if (myPlayer.Shooted) {
                    myPlayer.Shooted = false;
                    string message = "2¶" + myPlayer.Bullet.ToString();
                    packages.Enqueue(message);
                    continue;
                }
            }
        }
        
        static void ReceiveMessage() {
            UdpClient listener = new UdpClient(localListenPort, AddressFamily.InterNetwork);
            IPEndPoint remoteIP = null;
            while (true)
            {
                try {

                    byte[] data = listener.Receive(ref remoteIP);
                    receivedPackaged.Enqueue(Encoding.Unicode.GetString(data));

                } catch (Exception e) {
                    Console.WriteLine("Подключение прервано!");
                    Console.WriteLine(e);
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }

        static void Handler() {
            while (true) {
                if (receivedPackaged.Count > 0) {
                    string[] package = receivedPackaged.Dequeue().Split('¶');

                    #region Обработчик пакетов
                    if (package[0] == "0" && package.Length == 11) { //первый пакет

                        string name = package[1];
                        Point2D point = new Point2D(Int32.Parse(package[2]), Int32.Parse(package[3]));
                        ConsoleColor color = (ConsoleColor)Int32.Parse(package[4]);
                        Statistics stat = new Statistics(Int32.Parse(package[5]),
                            Int32.Parse(package[6]),
                            Int32.Parse(package[7]),
                            Int32.Parse(package[8]));
                        Direction direction = (Direction)Int32.Parse(package[9]);
                        int id = Int32.Parse(package[10]);

                        players.Add(id, new Player(name, point, color, stat, direction));

                        screen.updateTitle();
                    } else if (package[0] == "1" && package.Length == 3) {// движение

                        int id = Int32.Parse(package[2]);
                        players[id].move((Direction)Int32.Parse(package[1]), false);

                    } else if (package[0] == "2" && package.Length == 9) {// выстрел

                        int damage = Convert.ToInt32(package[1]);
                        int speed = Convert.ToInt32(package[2]);
                        char icon = Convert.ToChar(package[3]);
                        Point2D point = new Point2D(Int32.Parse(package[4]), Int32.Parse(package[5]));
                        Direction direction = (Direction)Convert.ToInt32(package[6]);
                        ConsoleColor color = (ConsoleColor)Convert.ToInt32(package[7]);
                        int id = Int32.Parse(package[8]);

                        players[id].Bullet = new Bullet(point, color, direction, icon, speed, damage);
                        players[id].shot();

                    } else if (package[0] == "3" && package.Length == 3){// симулируется попадание
                        int idWhom = Int32.Parse(package[1]);
                        int idShooter = Int32.Parse(package[2]);
                        try {                            
                            players[idWhom].Stat.Health -= players[idShooter].Bullet.Damage;
                            if (players[idWhom].Stat.Health <= 0) {
                                players[idWhom].Stat.Health = players[idWhom].Stat.MaxHealth;
                                players[idWhom].Stat.Deaths++;
                                players[idShooter].Stat.Kills++;
                            }
                            players[idShooter].Bullet.Alive = false;

                        } catch { // если ключа нет, значит это я

                            myPlayer.Stat.Health -= players[idShooter].Bullet.Damage;
                            if (myPlayer.Stat.Health <= 0) {
                                myPlayer.Stat.Health = myPlayer.Stat.MaxHealth;
                                myPlayer.Stat.Deaths++;
                                if (players[idShooter].Stat.Health + 2 <= players[idShooter].Stat.MaxHealth) {
                                    players[idShooter].Stat.Health += 2;
                                } else {
                                    players[idShooter].Stat.Health = players[idShooter].Stat.MaxHealth;
                                }
                                players[idShooter].Stat.Kills++;
                            }
                            players[idShooter].Bullet.Alive = false;
                        }
                    } else if (package[0] == "4" && package.Length == 4 || package.Length == 3) {// воскрешение (3-себя)
                        int x = Int32.Parse(package[1]);
                        int y = Int32.Parse(package[2]);

                        if (package.Length == 4) {
                            int id = Int32.Parse(package[3]);
                            players[id].revive(new Point2D(x, y));
                        } else {
                            myPlayer.revive(new Point2D(x, y));
                        }
                    } else if (package[0] == "5" && package.Length == 1) {
                        packages.Enqueue("5");
                    } else if (package[0] == "6" && package.Length == 2) {
                        int id = Int32.Parse(package[1]);
                        players[id].clear(true);
                        players.Remove(id);
                        screen.RankingWindow.RankingList.Remove(id);
                        screen.RankingWindow.Changed = true;
                        screen.updateTitle();
                    } 
                    continue;
                    #endregion
                    
                }
                Thread.Sleep(1);
            }
        }

        static void SendMessage() {
            while (true) {
                if (packages.Count > 0) {
                    byte[] data = Encoding.Unicode.GetBytes(packages.Dequeue());
                    sender.Send(data, data.Length);
                    continue;
                }
                Thread.Sleep(1);
            }
        }
         
        static void Disconnect() {
            Environment.Exit(0); //завершение процесса
        }

        static void ThreadScreen() {
            while (true) {
                if (screen.Changed) {
                    screen.Changed = false;
                    if (Console.Title != screen.Title)
                        Console.Title = screen.Title;                    
                }
                if (screen.FieldWindow.Changed) {
                    screen.FieldWindow.Changed = false;
                    screen.FieldWindow.show();
                }
                if (screen.RankingWindow.Changed) {
                    screen.RankingWindow.Changed = false;
                    screen.RankingWindow.show();
                }
                if (myPlayer.Changed) {
                    myPlayer.Changed = false;
                    myPlayer.clear();
                    myPlayer.show();                    
                }
                if (myPlayer.Stat.Changed) {
                    screen.RankingWindow.generateList();
                    screen.PlayerInfoWindow.show();
                    myPlayer.Stat.Changed = false;
                }
                if (myPlayer.Bullet.Changed) {                    
                    if (myPlayer.Bullet.Alive) {
                        myPlayer.Bullet.clear();
                        myPlayer.Bullet.show();
                    } else {
                        myPlayer.Bullet.clear(true);
                    }
                    myPlayer.Bullet.Changed = false;
                }
                for (int i = 0; i < players.Count; ++i) {
                    if(players.ElementAt(i).Value.Changed) {
                        players.ElementAt(i).Value.Changed = false;
                        players.ElementAt(i).Value.clear();
                        players.ElementAt(i).Value.show();                        
                    }
                    if (players.ElementAt(i).Value.Stat.Changed) {
                        screen.RankingWindow.generateList();
                    }
                    if (players.ElementAt(i).Value.Bullet.Changed) {
                        if (players.ElementAt(i).Value.Bullet.Alive) {
                            players.ElementAt(i).Value.Bullet.clear();
                            players.ElementAt(i).Value.Bullet.show();
                        } else {
                            players.ElementAt(i).Value.Bullet.clear(true);
                        }
                        players.ElementAt(i).Value.Bullet.Changed = false;
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
