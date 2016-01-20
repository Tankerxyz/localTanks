using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {

    class Bullet {

        public Bullet(Point2D point,            
            ConsoleColor color,
            Direction direction = Direction.up,
            char icon = '∙',
            int speed = 980,
            int damage = 1) 
        {            
            this.point = point;

            this.oldPoint = new Point2D(this.point.X, this.point.Y);

            this.direction = direction;
            this.color = color;
            this.icon = icon;
            this.speed = speed;
            this.damage = damage;
            this.alive = false;
            this.Changed = false;            
        }

        public void move() {
            oldPoint.X = point.X;
            oldPoint.Y = point.Y;
            if (direction == Direction.up) {                
                --point.Y;
            } else if (direction == Direction.down) {
                ++point.Y;
            } else if (direction == Direction.left) {
                --point.X;
            } else if (direction == Direction.right) {
                ++point.X;
            }
        }

        public void show() {

            ConsoleColor temp = Console.ForegroundColor;
            Console.SetCursorPosition(X, Y);            
            Console.ForegroundColor = color;
            Console.Write(icon);

        }

        public void clear(bool both = false) {

            Console.SetCursorPosition(oldPoint.X, oldPoint.Y);
            Console.BackgroundColor = Program.backgroundColor;
            Console.Write(' ');
            if (both == true) {
                Console.SetCursorPosition(X, Y);
                Console.BackgroundColor = Program.backgroundColor;
                Console.Write(' ');
            }

        }

        public override string ToString() {
            return String.Format(
                "{0}¶{1}¶{2}¶{3}¶{4}¶{5}",
                damage,
                speed,
                icon,
                point.ToString(),
                (int)direction,
                (int)color
                );
        }
        
        public static bool operator==(Bullet bullet, TankModel m) {
            for (int i = 0; i < m.Model.Length; ++i) {
                if (bullet.Point == m.Model[i]) {
                    return true;
                }
            }
            return false;
        }

        public static bool operator!=(Bullet bullet, TankModel m) {
            for (int i = 0; i < m.Model.Length; ++i) {
                if (bullet.Point == m.Model[i]) {
                    return false;
                }
            }
            return true;
        }

        #region Поля
        public bool Changed;
        
        private int damage;
        public int Damage { get { return damage; } set { damage = value; } }

        private int speed;
        public int Speed { get { return speed; } set { speed = value; } }

        private char icon;
        public char Icon { get { return icon; } set { icon = value; } }

        private Point2D oldPoint;
        public Point2D OldPoint { get { return oldPoint; } set { oldPoint = value; } }


        private Point2D point;
        public Point2D Point { get { return point; } set { point = value; } }
        public int X { get { return point.X; } set { point.X = value; } }
        public int Y { get { return point.Y; } set { point.Y = value; } }

        private bool alive;
        public bool Alive { get { return alive; } set { alive = value; } }

        private Direction direction;
        public Direction Direction { get { return direction; } set { direction = value; } }

        private ConsoleColor color;
        public ConsoleColor Color { get { return color; } set { color = value; } }
#endregion
    }
}
