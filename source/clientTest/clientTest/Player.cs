using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace clientTest {
    
    enum Direction { up, down, left, right }
    class Player {

        static object locker = new object();

        public Player(string name, Point2D point, ConsoleColor color, Statistics stat, Direction direction = Direction.up, int speed = 1) {
            this.name = name;            
            this.point = point;
            this.oldPoint = new Point2D(this.point.X, this.point.Y);
            this.color = color;
            this.stat = stat;
            this.oldDirection = direction;
            this.direction = direction;
            this.speed = speed;
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

            if (timeToMove.ElapsedMilliseconds > ( 190 - (speed * 30)) ) {
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
                            shot();                 // выстрелил
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
            bullet.Damage = 1;
            speed = 1;

            oldPoint.X = X;
            oldPoint.Y = Y;
            oldDirection = direction;

            this.point.X = p.X;
            this.point.Y = p.Y;
            this.direction = Direction.up;
            model.generate(this.point, this.direction);
            this.Changed = true;
        }
        public void move(Direction d, bool check = true) {
            if (check) {
                Point2D nextMovePoint = new Point2D(point.X, point.Y);
                TankModel nextMoveModel = new TankModel();
                switch (d) {
                    case Direction.up:
                        if (nextMovePoint.Y <= 2)
                            return;
                        --nextMovePoint.Y; 
                        break;
                    case Direction.down:
                        if (point.Y >= Program.height - 2)
                            return;
                        ++nextMovePoint.Y; 
                        break;
                    case Direction.left:
                        if (nextMovePoint.X <= 2)
                            return;
                        --nextMovePoint.X;
                        break;
                    case Direction.right:
                        if (point.X >= Program.width - 2)
                            return;
                        ++nextMovePoint.X;
                        break;
                }
                nextMoveModel.generate(nextMovePoint, d);
                
                if (!canMove(d, nextMoveModel))
                    return;
            }
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
        public bool canMove(Direction d, TankModel nextMove) {
            foreach (var i in Program.players) {
                if (nextMove == i.Value.model) {
                    return false;
                }
            }
            return true;
        }
        public void up() {
            --point.Y;
        }
        public void down() {
            ++point.Y;
        }
        public void left() {
            --point.X;
        }
        public void right() {
            ++point.X;
        }

        public void shot() {
            bullet.Changed = true;
            bullet.Alive = true;
            Timer t = new Timer(new TimerCallback(timerShot));
            t.Change(1, 1000 - bullet.Speed);            
        }
        void timerShot(object obj) {

                //проверка на попадание
            if (!bullet.Alive || bullet.X >= Program.width - 1 || bullet.X <= 1 ||
                bullet.Y >= Program.height - 1 || bullet.Y <= 1) {
                isShooter = false;
                bullet.Alive = false;
                bullet.Changed = true;

                Timer t = (Timer)obj;
                t.Dispose();
                return;
            }
            
            if (isShooter) {
                for (int i = 0; i < Program.players.Count; ++i) {
                    if (bullet.Point == Program.players.ElementAt(i).Value.Model) {
                        
                        Program.packages.Enqueue("3¶" + Program.players.ElementAt(i).Key);
                        Program.players.ElementAt(i).Value.stat.Health -= bullet.Damage;
                        if (Program.players.ElementAt(i).Value.stat.Health <= 0) {
                            if(this.stat.Health + 2 <= this.stat.MaxHealth) {
                                this.stat.Health += 2;
                            } else {
                                this.stat.Health = this.stat.MaxHealth;
                            }
                            Program.players.ElementAt(i).Value.stat.Health = Program.players.ElementAt(i).Value.stat.MaxHealth;
                            Program.players.ElementAt(i).Value.stat.Deaths++;
                            this.stat.Kills++;
                        }
                        isShooter = false;
                        bullet.Alive = false;
                        bullet.Changed = true;
                        Timer t = (Timer)obj;
                        t.Dispose();

                        return;
                    }
                }
            }

            bullet.move();
            bullet.Changed = true;
        }


        #region Поля

        private int speed;
        public int Speed { get { return speed; } set { speed = value; } }

        private bool isShooter;
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
                    (int)direction );
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
