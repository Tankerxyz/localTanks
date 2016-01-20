using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace clientTest {
    class RankingTable : Window {

        public RankingTable(
            int width,
            int height,
            Point2D position,
            ConsoleColor foregroundColor = ConsoleColor.DarkGray,
            ConsoleColor backgroundColor = ConsoleColor.Black
            ) {

            this.width = width;
            this.height = height;
            this.position = position;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.margin = new MarginWithAngles();
            this.changed = true;
            rankingList = new Dictionary<int, RankingSlot>();
        }

        public override void show() {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;            
            for (int i = 0; i <= height; ++i) {
                Console.SetCursorPosition(Position.X, Position.Y + i);
                for (int j = 0; j <= width; ++j) {
                    if (i == 0 && j == 0) {
                        Console.Write(margin.UpLeft);
                    } else if (i == 0 && j == width) {
                        Console.Write(margin.UpRight);
                    } else if (i == height && j == width) {
                        Console.Write(margin.DownRight);
                    } else if (i == height && j == 0) {
                        Console.Write(margin.DownLeft);
                    } else if (i == 0) {
                        Console.Write(margin.Up);
                    } else if (i == height) {
                        Console.Write(margin.Down);
                    } else if (j == 0) {
                        Console.Write(margin.Left);
                    } else if (j == width) {
                        Console.Write(margin.Right);
                    } else {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            //получение ключей
            List<int> arr = new List<int>();
            while (arr.Count != rankingList.Count) {
                for (int i = 0; i < rankingList.Count; ++i) {
                    int maxKey = i;
                    if (arr.IndexOf(maxKey) != -1)
                        continue;
                    for (int j = i + 1; j < rankingList.Count; ++j) {
                        if (arr.IndexOf(j) == -1) {
                            if (maxKey != j) {
                                if (rankingList.ElementAt(maxKey).Value < rankingList.ElementAt(j).Value) {
                                    maxKey = j;
                                }
                            }
                        }
                    }
                    arr.Add(maxKey);
                    break;
                }
            }

            int lenght = 0;
            if (rankingList.Count < height) {
                lenght = rankingList.Count;
            } else {
                lenght = height - 1;
            }

            for (int i = 0; i < lenght; ++i) {
                if (i == 0)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else if (i == 1)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (i == 2)
                    Console.ForegroundColor = ConsoleColor.White;

                string name = rankingList.ElementAt(arr[i]).Value.Name;
                int kills = rankingList.ElementAt(arr[i]).Value.Kills;
                int deaths = rankingList.ElementAt(arr[i]).Value.Deaths;

                Console.SetCursorPosition(position.X + 1, (position.Y + 1) + i);
                Console.Write(rankingList.ElementAt(arr[i]).Value.Name);                
                int dotLenght = (width - 2) - name.Length - ((kills.ToString().Length + deaths.ToString().Length) + 1);
                for (int j = 0; j < dotLenght; ++j) {
                    Console.Write('.');
                }
                Console.Write("{0}/{1}", kills, deaths);
                Console.ResetColor();
            }
        }

        public override void clear() {
            throw new NotImplementedException();
        }

        public void generateList() {
            bool flag = false;
            if (Program.myPlayer.Stat.Changed) {
                Program.myPlayer.Stat.Changed = false;
                try {
                    rankingList[0].Name = Program.myPlayer.Name;
                    rankingList[0].Kills = Program.myPlayer.Stat.Kills;
                    rankingList[0].Deaths = Program.myPlayer.Stat.Deaths;
                } catch {
                    rankingList.Add(0, new RankingSlot(Program.myPlayer.Name, Program.myPlayer.Stat.Kills, Program.myPlayer.Stat.Deaths));
                }
                flag = true;
            }

            for (int i = 0; i < Program.players.Count; ++i) {
                Program.players.ElementAt(i).Value.Stat.Changed = false;
                string name = Program.players.ElementAt(i).Value.Name;
                int kills = Program.players.ElementAt(i).Value.Stat.Kills;
                int deaths = Program.players.ElementAt(i).Value.Stat.Deaths;
                int key = Program.players.ElementAt(i).Key;

                try {
                    rankingList[key].Deaths = deaths;
                    rankingList[key].Kills = kills;
                    rankingList[key].Name = name;
                } catch {
                    rankingList.Add(key, new RankingSlot(name, kills, deaths));
                }             
            }
            changed = flag;
        }
        
        public Dictionary<int, RankingSlot> rankingList;
        public Dictionary<int, RankingSlot> RankingList { get { return rankingList; } }
    }
}
