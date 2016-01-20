using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class RankingSlot {

        public RankingSlot(string name, int kills, int deaths) {
            this.name = name;
            this.kills = kills;
            this.deaths = deaths;
        }

        public static bool operator>(RankingSlot a, RankingSlot b) {
            if (a.kills > b.kills) {
                return true;
            } else if (a.kills == b.kills) {
                if (a.deaths > b.deaths) {
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }

        public static bool operator <(RankingSlot a, RankingSlot b) {
            if (a.kills < b.kills) {
                return true;
            } else if (a.kills == b.kills) {
                if (a.deaths < b.deaths) {
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private int kills;
        public int Kills { get { return kills; } set { kills = value; } }

        private int deaths;
        public int Deaths { get { return deaths; } set { deaths = value; } }
    }
}
