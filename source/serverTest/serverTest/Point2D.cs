using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverTest {
    class Point2D {
        public Point2D(int x = 0, int y = 0) {
            this.x = x;
            this.y = y;
        }

        public override string ToString() {
            return String.Format("{0}¶{1}", x, y);
        }

        public static bool operator ==(Point2D a, Point2D b) {
            return ((a.X == b.X) && (a.Y == b.Y));
        }

        public static bool operator !=(Point2D a, Point2D b) {
            return ((a.X != b.X) && (a.Y != b.Y));
        }

        public static bool operator ==(Point2D a, TankModel b) {
            for (int i = 0; i < b.Model.Length; ++i) {
                if (a == b.Model[i]) {
                    return true;
                }
            }
            return false;
        }

        public static bool operator !=(Point2D a, TankModel b) {
            for (int i = 0; i < b.Model.Length; ++i) {
                if (a != b.Model[i]) {
                    return true;
                }
            }
            return false;
        }

        private int x;
        public int X { get { return x; } set { x = value; } }

        private int y;
        public int Y { get { return y; } set { y = value; } }
    }
}
