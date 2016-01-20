using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class Screen{

        public Screen(FieldWindow fieldWindow, RankingTable rankingWindow, PlayerInfoWindow playerInfoWindow, string title = "") {
            this.fieldWindow = fieldWindow;
            this.rankingWindow = rankingWindow;
            this.playerInfoWindow = playerInfoWindow;
            this.title = title;
            this.changed = true;
        }

        public void updateTitle() {
            Title = String.Format("LoTanks                                         {0} Сервер {1}:{2} Игроков:{3}", Program.myPlayer.Name, Program.host, Program.serverPort, Program.players.Count + 1);
        }

        private FieldWindow fieldWindow;
        public FieldWindow FieldWindow { get { return fieldWindow; } set { fieldWindow = value; } }

        private RankingTable rankingWindow;
        public RankingTable RankingWindow { get { return rankingWindow; } set { rankingWindow = value; } }

        private PlayerInfoWindow playerInfoWindow;
        public PlayerInfoWindow PlayerInfoWindow { get { return playerInfoWindow; } set { playerInfoWindow = value; } }

        private string title;
        public string Title { get { return title; } set { title = value; changed = true; } }

        private bool changed;
        public bool Changed { get { return changed; } set { changed = value; } }
    }
}
