using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverTest {
    class TankModel {

        public TankModel() {
            this.color = ConsoleColor.White;
            this.label = ' ';
            this.model = new Point2D[6];
            this.oldModel = new Point2D[6];

            for (int i = 0; i < this.model.Length; ++i) {
                model[i] = new Point2D();
                oldModel[i] = new Point2D();
            }
        }

        public TankModel(Point2D p, Direction d, ConsoleColor color, char label) {
            this.color = color;
            this.label = label;
            this.model = new Point2D[6];
            this.oldModel = new Point2D[6];

            for (int i = 0; i < this.model.Length; ++i) {
                model[i] = new Point2D();
                oldModel[i] = new Point2D();
            }
            generate(p, d);
        }

        // Метод для отрисовки модели в зависимости от направления
        // p - центральная координата
        public void show() {

            Console.BackgroundColor = color;

            Console.ForegroundColor = ConsoleColor.Black;//
            for (int i = 0; i < model.Length; ++i) {
                Console.SetCursorPosition(model[i].X, model[i].Y);
                // Нахождение центра
                if (model[i].X == center.X && model[i].Y == center.Y) {
                    Console.Write(label);
                } else {
                    Console.Write(" ");
                }
            }
            Console.ResetColor();
        }

        public void clear(bool both = false) {

            Console.BackgroundColor = Program.backgroundColor;
            for (int i = 0; i < oldModel.Length; ++i) {
                if (oldModel[i].X == 0)
                    return;
                Console.SetCursorPosition(oldModel[i].X, oldModel[i].Y);
                Console.Write(" ");
            }

            if (both) {
                Console.BackgroundColor = Program.backgroundColor;
                for (int i = 0; i < model.Length; ++i) {
                    if (model[i].X == 0)
                        return;
                    Console.SetCursorPosition(model[i].X, model[i].Y);
                    Console.Write(" ");
                }
            }
        }

        public void generate(Point2D p, Direction d) {
            center.X = p.X;
            center.Y = p.Y;
            for (int i = 0; i < model.Length; ++i) {
                oldModel[i].X = model[i].X;
                oldModel[i].Y = model[i].Y;
            }

            if (d == Direction.up) {
                #region up generate
                //   [+]
                //[ ][ ][ ]
                //[ ]   [ ]
                model[0].X = p.X;
                model[0].Y = p.Y - 1;

                //   [ ]
                //[+][ ][ ]
                //[ ]   [ ]
                model[1].X = p.X - 1;
                model[1].Y = p.Y;

                //   [ ]
                //[ ][+][ ]
                //[ ]   [ ]
                model[2].X = p.X;
                model[2].Y = p.Y;

                //   [ ]
                //[ ][ ][+]
                //[ ]   [ ]
                model[3].X = p.X + 1;
                model[3].Y = p.Y;

                //   [ ]
                //[ ][ ][ ]
                //[+]   [ ]
                model[4].X = p.X - 1;
                model[4].Y = p.Y + 1;

                //   [ ]
                //[ ][ ][ ]
                //[ ]   [+]
                model[5].X = p.X + 1;
                model[5].Y = p.Y + 1;
                #endregion
            } else if (d == Direction.down) {
                #region down generate
                //[ ]   [ ]
                //[ ][ ][ ]
                //   [+]   
                model[0].X = p.X;
                model[0].Y = p.Y + 1;

                //[ ]   [ ]
                //[+][ ][ ]
                //   [ ]  
                model[1].X = p.X - 1;
                model[1].Y = p.Y;

                //[ ]   [ ]
                //[ ][+][ ]
                //   [ ]  
                model[2].X = p.X;
                model[2].Y = p.Y;

                //[ ]   [ ]
                //[ ][ ][+]
                //   [ ]  
                model[3].X = p.X + 1;
                model[3].Y = p.Y;

                //[+]   [ ]
                //[ ][ ][ ]
                //   [ ]  
                model[4].X = p.X - 1;
                model[4].Y = p.Y - 1;

                //[ ]   [+]
                //[ ][ ][ ]
                //   [ ]  
                model[5].X = p.X + 1;
                model[5].Y = p.Y - 1;
                #endregion
            } else if (d == Direction.left) {
                #region left generate
                //   [+][ ]
                //[ ][ ]
                //   [ ][ ]
                model[0].X = p.X;
                model[0].Y = p.Y - 1;

                //   [ ][+]
                //[ ][ ]
                //   [ ][ ]
                model[1].X = p.X + 1;
                model[1].Y = p.Y - 1;

                //   [ ][ ]
                //[+][ ]
                //   [ ][ ] 
                model[2].X = p.X - 1;
                model[2].Y = p.Y;

                //   [ ][ ]
                //[ ][+]
                //   [ ][ ] 
                model[3].X = p.X;
                model[3].Y = p.Y;

                //   [ ][ ]
                //[ ][ ]
                //   [+][ ]
                model[4].X = p.X;
                model[4].Y = p.Y + 1;

                //   [ ][ ]
                //[ ][ ]
                //   [ ][+] 
                model[5].X = p.X + 1;
                model[5].Y = p.Y + 1;
                #endregion
            } else if (d == Direction.right) {
                #region right generate
                //[+][ ]   
                //   [ ][ ]
                //[ ][ ]
                model[0].X = p.X - 1;
                model[0].Y = p.Y - 1;

                //[ ][+]   
                //   [ ][ ]
                //[ ][ ]
                model[1].X = p.X;
                model[1].Y = p.Y - 1;

                //[ ][ ]   
                //   [+][ ]
                //[ ][ ]
                model[2].X = p.X;
                model[2].Y = p.Y;

                //[ ][ ]   
                //   [ ][+]
                //[ ][ ] 
                model[3].X = p.X + 1;
                model[3].Y = p.Y;

                //[ ][ ]   
                //   [ ][ ]
                //[+][ ]
                model[4].X = p.X - 1;
                model[4].Y = p.Y + 1;

                //[ ][ ]   
                //   [ ][ ]
                //[ ][+]
                model[5].X = p.X;
                model[5].Y = p.Y + 1;
                #endregion
            }
        }

        public static bool operator ==(TankModel a, TankModel b) {
            for (int i = 0; i < a.Model.Length; ++i) {
                for (int j = 0; j < b.Model.Length; ++j) {
                    if (a.Model[i] == b.Model[j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool operator !=(TankModel a, TankModel b) {
            for (int i = 0; i < a.Model.Length; ++i) {
                for (int j = 0; j < b.Model.Length; ++j) {
                    if (a.Model[i] == b.Model[j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        private ConsoleColor color;
        public ConsoleColor Color { get { return color; } set { color = value; } }

        // Для отрисовки символа
        private char label;
        public char Label { get { return label; } set { label = value; } }

        private Point2D center = new Point2D();

        private Point2D[] model;
        public Point2D[] Model { get { return model; } set { model = value; } }

        private Point2D[] oldModel;
        public Point2D[] OldModel { get { return model; } set { model = value; } }
    }
}
