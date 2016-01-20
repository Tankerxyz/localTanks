using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace serverTest {
    public class ServerObject {

        // 0 - первый пакет
        // 1 - пакет движения
        // 2 - пакет стрельбы
        // 3 - пакет попадания
        // 4 - пакет возрождения
        // 5 - пакет подтверждения прослушки от клиента
        
        static int generateID = 0;
        static volatile Dictionary<int, ClientObject> clients = new Dictionary<int,ClientObject>();

        static int port = 8888;
        static volatile UdpClient receiver = new UdpClient(port);
        static volatile UdpClient sender = new UdpClient();
        static volatile Queue<Package> packages = new Queue<Package>();
        static Random rand = new Random();
        static object locker = new object();
    
        // прослушивание входящих подключений
        public void Listen() {
            
            Thread handlerThread = new Thread( new ThreadStart(Handler) );
            handlerThread.Start();

            Timer timerChecker = new Timer(new TimerCallback(chekerConnections));
            timerChecker.Change(1000, 1000);

            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            updateTitle();

            IPEndPoint remoteIp = null;
            try {
                while (true) {

                    byte[] data = receiver.Receive(ref remoteIp);
                    packages.Enqueue(new Package(Encoding.Unicode.GetString(data), new IPEndPoint(remoteIp.Address, remoteIp.Port)));
                    
                }

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);
                Environment.Exit(0);

            }
        }

        public void Handler() {
            while(true) {
                if (packages.Count > 0) {
                    Package temp = packages.Dequeue();

                    IPEndPoint remoteIp = temp.Ip;
                    string message = temp.Message;
                    string[] package = message.Split('¶');

                    #region Обработчик пакетов
                    if (package[0] == "0" && package.Length == 4) {
                        string name = package[1];
                        ConsoleColor color = (ConsoleColor)Int32.Parse(package[2]);
                        int port = Int32.Parse(package[3]);

                        
                        

                        Point2D point;
                        TankModel tankModel;
                        lock(locker) {                            
                            while (true) {
                                bool flag = true;
                                point = new Point2D(rand.Next(3, Program.width - 3), rand.Next(3, Program.height - 3));
                                tankModel = new TankModel();
                                tankModel.generate(point, Direction.up);
                                foreach (var i in clients) {
                                    if(i.Value.Player.Model == tankModel) {
                                        flag = false;
                                    }
                                }
                                if (flag) {
                                    break;
                                }
                            }
                        }
                        Statistics stat = new Statistics(0, 0, 5, 5);
                        Direction direction = Direction.up;
                        
                        IPEndPoint reallyRemoteIp = new IPEndPoint(remoteIp.Address, port);
                        ClientObject newClient = new ClientObject(reallyRemoteIp, new Player(name, point, color, stat, direction));

                        clients.Add(++generateID, newClient);

                        Console.WriteLine("{0} | Игрок: {1}: {2} - подключился ", DateTime.Now.ToLongTimeString(), name, remoteIp);
                        updateTitle();

                        byte[] data = Encoding.Unicode.GetBytes(String.Format("4¶{0}¶{1}", point.X, point.Y));
                        sender.Send(data, data.Length, clients[generateID].IP);

                        message = "0¶" + clients[generateID].Player.Info;
                        // отослать всех игроков, только что зашедшему
                        BroadcastMessage(message + "¶" + generateID, generateID, true);

                        BroadcastMessage(message + "¶" + generateID, generateID);

                    } else if (package[0] == "1" && package.Length == 2) {

                        int id = 0;
                        foreach (var i in clients) {
                            if (i.Value.IP.Address.ToString() == remoteIp.Address.ToString() &&
                                i.Value.IP.Port.ToString() == (remoteIp.Port - 1).ToString()) {

                                i.Value.Player.move((Direction)Int32.Parse(package[1]));
                                id = i.Key;
                                break;
                            }
                        }
                        BroadcastMessage(message + "¶" + id, id);

                    } else if (package[0] == "2" && package.Length == 8) {

                        int id = 0;
                        foreach (var i in clients) {
                            if (i.Value.IP.Address.ToString() == remoteIp.Address.ToString() &&
                                i.Value.IP.Port.ToString() == (remoteIp.Port - 1).ToString()) {

                                id = i.Key;
                                break;
                            }
                        }
                        BroadcastMessage(message + "¶" + id, id);

                    } else if (package[0] == "3" && package.Length == 2) { // попадание
                        int idWhom = Int32.Parse(package[1]);
                        int idShooter = 0;
                        foreach (var i in clients) {
                            if (i.Value.IP.Address.ToString() == remoteIp.Address.ToString() &&
                                i.Value.IP.Port.ToString() == (remoteIp.Port - 1).ToString()) {

                                idShooter = i.Key;
                                break;
                            }
                        }

                        BroadcastMessage(message + "¶" + idShooter, idShooter);

                        clients[idWhom].Player.Stat.Health -= clients[idShooter].Player.Bullet.Damage;
                        if (clients[idWhom].Player.Stat.Health <= 0) {
                            clients[idWhom].Player.Stat.Health = clients[idWhom].Player.Stat.MaxHealth;
                            clients[idWhom].Player.Stat.Deaths++;
                            if (clients[idShooter].Player.Stat.Health + 2 <= clients[idShooter].Player.Stat.MaxHealth) {
                                clients[idShooter].Player.Stat.Health += 2;
                            } else {
                                clients[idShooter].Player.Stat.Health = clients[idShooter].Player.Stat.MaxHealth;
                            }
                            clients[idShooter].Player.Stat.Kills++;

                            Point2D point;
                            TankModel tankModel;
                            lock (locker) {
                                while (true) {
                                    bool flag = true;
                                    point = new Point2D(rand.Next(3, Program.width - 3), rand.Next(3, Program.height - 3));
                                    tankModel = new TankModel();
                                    tankModel.generate(point, Direction.up);
                                    foreach (var i in clients) {
                                        if (i.Value.Player.Model == tankModel) {
                                            flag = false;
                                        }
                                    }
                                    if (flag) {
                                        break;
                                    }
                                }
                            }

                            clients[idWhom].Player.revive(new Point2D(point.X, point.Y));

                            BroadcastMessage(String.Format("4¶{0}¶{1}¶{2}", point.X, point.Y, idWhom), idWhom);
                            byte[] data = Encoding.Unicode.GetBytes(String.Format("4¶{0}¶{1}", point.X, point.Y));
                            sender.Send(data, data.Length, clients[idWhom].IP);

                            Console.WriteLine("{0} | Игрок {1} убил игрока {2}", DateTime.Now.ToLongTimeString(), clients[idShooter].Player.Name, clients[idWhom].Player.Name);

                        }

                    } else if(package[0] == "5" && package.Length == 1) {
                        
                        for (int i = 0; i < clients.Count; ++i) {
                            if (clients.ElementAt(i).Value.IP.Address.ToString() == remoteIp.Address.ToString() &&
                                clients.ElementAt(i).Value.IP.Port.ToString() == (remoteIp.Port - 1).ToString()) {

                                clients.ElementAt(i).Value.Connected = true;
                            }
                        }
                    }
                    #endregion
                    continue;
                } else {
                    Thread.Sleep(1);
                }
            }
        }

        static void updateTitle() {
            Console.Title = String.Format("LoTanks Server                    Адрес: {0}:{1}        Игроков: {2}",Dns.GetHostByName(Dns.GetHostName()).AddressList[0], port, clients.Count);
        }

        // прослушка
        static void chekerConnections(object obj) {
            if (clients.Count > 0) {
                lock (locker) {
                    for (int i = 0; i < clients.Count; ++i) {
                        clients.ElementAt(i).Value.Connected = false;
                    }
                    BroadcastMessage("5", -1); //отправка всем прослушивающего пакета
                }
                Thread.Sleep(500);
                lock (locker) {
                    for (int i = 0; i < clients.Count; ++i) {
                        if (clients.ElementAt(i).Value.Connected == false) {
                            Console.WriteLine("{0} | Игрок: {1}: {2} - отключился ", DateTime.Now.ToLongTimeString(), clients.ElementAt(i).Value.Player.Name, clients.ElementAt(i).Value.IP);
                            // отправка id который отключился, чтобы клиенты его удалили
                            BroadcastMessage("6¶" + clients.ElementAt(i).Key, clients.ElementAt(i).Key);
                            clients.Remove(clients.ElementAt(i).Key);
                            updateTitle();
                                                        
                        }
                    }
                }
            }
        }

        // трансляция сообщения подключенным клиентам
        public static void BroadcastMessage(string message, int id, bool toMe = false ) {

            byte[] data = Encoding.Unicode.GetBytes(message);
            lock (locker) {
                if (!toMe) {
                    foreach (var i in clients) {
                        if (i.Key != id) {
                            sender.Send(data, data.Length, i.Value.IP);
                        }
                    }
                } else { //Отправка всех клиентов одному клиенту
                    foreach (var i in clients) {
                        if (i.Key != id) {
                            string pack = ("0¶" + i.Value.Player.Info + "¶" + i.Key);
                            byte[] tData = Encoding.Unicode.GetBytes(pack);
                            sender.Send(tData, tData.Length, clients[id].IP);
                        }
                    }
                }
            }

        }

    }
}
