using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverTest {
    class Statistics {
        public Statistics(int kills = 0, int deaths = 0, int health = 5, int maxHealth = 5) {
            this.kills = kills;
            this.deaths = deaths;
            this.health = health;
            this.maxHealth = maxHealth;
        }

        public override string ToString() {
            return String.Format("{0}¶{1}¶{2}¶{3}", kills, deaths, health, maxHealth);
        }

        private int kills;
        public int Kills { get { return kills; } set { kills = value; } }

        private int deaths;
        public int Deaths { get { return deaths; } set { deaths = value; } }

        private int health;
        public int Health { get { return health; } set { health = value; } }

        private int maxHealth;
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }        
    }
}
