using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace serverTest {

    enum Direction { up, down, left, right }
    class Player {

        public Player(string name, Point2D point, ConsoleColor color, Statistics stat, Direction direction = Direction.up) {
            this.name = name;
            this.point = point;
            this.oldPoint = new Point2D(this.point.X, this.point.Y);
            this.color = color;
            this.stat = stat;
            this.oldDirection = direction;
            this.direction = direction;
            this.Changed = true;
            this.isShooter = false;
            this.model = new TankModel(new Point2D(point.X, point.Y), direction, color, name[0]);
            this.bullet = new Bullet(new Point2D(X, Y), model.Color, direction);
            this.timeToMove = new Stopwatch();
            this.timeToShoot = new Stopwatch();

            timeToMove.Start();
            timeToShoot.Start();
        }

        public void run() {

            cKey = Console.ReadKey(true).Key;
            
            if (timeToMove.ElapsedMilliseconds > 250) {
                timeToMove.Restart();
                switch (cKey) {

                    case ConsoleKey.UpArrow:
                        move(Direction.up);
                        break;

                    case ConsoleKey.DownArrow:
                        move(Direction.down);
                        break;

                    case ConsoleKey.LeftArrow:
                        move(Direction.left);
                        break;

                    case ConsoleKey.RightArrow:
                        move(Direction.right);
                        break;

                    case ConsoleKey.Spacebar:
                        // Проверка запущена ли пуля
                        if (!bullet.Alive && timeToShoot.ElapsedMilliseconds > 500) {
                            timeToShoot.Restart();
                            switch (direction) {
                                case Direction.up:
                                    bullet = new Bullet(new Point2D(X, Y - 2), color, direction); break;
                                case Direction.down:
                                    bullet = new Bullet(new Point2D(X, Y + 2), color, direction); break;
                                case Direction.left:
                                    bullet = new Bullet(new Point2D(X - 2, Y), color, direction); break;
                                case Direction.right:
                                    bullet = new Bullet(new Point2D(X + 2, Y), color, direction); break;
                            }
                            Thread.Sleep(1);

                            this.isShooter = true;  // подумал
                            //shot();                 // выстрелил
                            this.shooted = true;    // осознал

                        }
                        break;
                }
            }
        }

        public void show() {
            model.show();
        }
        public void clear(bool both = false) {
            model.clear(both);
        }

        public void revive(Point2D p) {
            oldPoint.X = X;
            oldPoint.Y = Y;
            oldDirection = direction;

            this.point.X = p.X;
            this.point.Y = p.Y;
            this.direction = Direction.up;
            this.Changed = true;
        }
        public void move(Direction d) {

            oldPoint.X = X;
            oldPoint.Y = Y;
            oldDirection = direction;

            switch (d) {
                case Direction.up:
                    up();
                    break;
                case Direction.down:
                    down();
                    break;
                case Direction.left:
                    left();
                    break;
                case Direction.right:
                    right();
                    break;
            }
            direction = d;
            model.generate(point, direction);
            this.Changed = true;

        }
        public void up() {
            if (point.Y > 2)
                --point.Y;
        }
        public void down() {
            if (point.Y < Program.height - 2)
                ++point.Y;
        }
        public void left() {
            if (point.X > 2)
                --point.X;
        }
        public void right() {
            if (point.X < Program.width - 2)
                ++point.X;
        }

                        


        #region Поля

        public bool isShooter;
        public bool Ishooter { get { return isShooter; } set { isShooter = value; } }

        private ConsoleKey cKey;
        private Stopwatch timeToShoot;
        private Stopwatch timeToMove;
        public bool Changed;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        // необходимо для очистки по прошлым координатам
        private Point2D oldPoint;
        public Point2D OldPoint { get { return oldPoint; } set { oldPoint = value; } }
        private Direction oldDirection;
        public Direction OldDirection { get { return oldDirection; } set { oldDirection = value; } }

        private Point2D point;
        public Point2D Point { get { return point; } set { point = value; } }
        public int X { get { return point.X; } set { point.X = value; } }
        public int Y { get { return point.Y; } set { point.Y = value; } }

        private ConsoleColor color;
        public ConsoleColor Color { get { return color; } set { color = value; } }

        private TankModel model;
        public TankModel Model { get { return model; } set { model = value; } }

        //private TankModel model;
        //public TankModel Model { get { return model; } set { model = value; } }

        private Statistics stat;
        public Statistics Stat { get { return stat; } set { stat = value; } }

        private Bullet bullet;
        public Bullet Bullet { get { return bullet; } set { bullet = value; } }

        private Direction direction;
        public Direction Direction { get { return direction; } set { direction = value; } }

        private bool shooted;
        public bool Shooted { get { return shooted; } set { shooted = value; } }

        public string Info {
            get {
                return String.Format(
                    "{0}¶{1}¶{2}¶{3}¶{4}",
                    name,
                    point.ToString(),
                    (int)color,
                    stat.ToString(),
                    (int)direction);
            }
        }

        public string SmallInfo {
            get {
                return String.Format(
                    "{0}¶{1}",
                    point.ToString(),
                    (int)direction);
            }
        }
        #endregion
    }
}
