using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class DotModel {
        public DotModel(Point2D p, Direction d, ConsoleColor color, char label) {
            this.color = color;
            this.label = label;
            this.model = new Point2D[6];

            for (int i = 0; i < this.model.Length; ++i) {
                model[i] = new Point2D();
            }
            model[0] = p;

            //generate(p, d);
        }

        // Метод для отрисовки модели в зависимости от направления
        // p - центральная координата
        public void show(Point2D p, Direction d) {

            Console.BackgroundColor = color;
            Console.ForegroundColor = Program.foregroundColor;
            Console.SetCursorPosition(p.X, p.Y);
            Console.Write(label);
            Console.BackgroundColor = Program.backgroundColor;
        }

        public void clear(Point2D oldPoint) {
            Console.BackgroundColor = Program.backgroundColor;
            Console.SetCursorPosition(oldPoint.X, oldPoint.Y);
            Console.Write(" ");
        }

        public void generate(Point2D p, Direction d) {
            if (d == Direction.up) {
                #region up generate
                //   [+]
                //[ ][ ][ ]
                //[ ]   [ ]
                model[0].X = p.X; model[0].Y = p.Y - 1;

                //   [ ]
                //[+][ ][ ]
                //[ ]   [ ]
                model[1].X = p.X - 1; model[1].Y = p.Y;

                //   [ ]
                //[ ][+][ ]
                //[ ]   [ ]
                model[2].X = p.X; model[2].Y = p.Y;

                //   [ ]
                //[ ][ ][+]
                //[ ]   [ ]
                model[3].X = p.X + 1; model[3].Y = p.Y;

                //   [ ]
                //[ ][ ][ ]
                //[+]   [ ]
                model[4].X = p.X - 1; model[4].Y = p.Y + 1;

                //   [ ]
                //[ ][ ][ ]
                //[ ]   [+]
                model[5].X = p.X + 1; model[5].Y = p.Y + 1;
                #endregion
            } else if (d == Direction.down) {
                #region down generate
                //[ ]   [ ]
                //[ ][ ][ ]
                //   [+]   
                model[0].X = p.X; model[0].Y = p.Y + 1;

                //[ ]   [ ]
                //[+][ ][ ]
                //   [ ]  
                model[1].X = p.X - 1; model[1].Y = p.Y;

                //[ ]   [ ]
                //[ ][+][ ]
                //   [ ]  
                model[2].X = p.X; model[2].Y = p.Y;

                //[ ]   [ ]
                //[ ][ ][+]
                //   [ ]  
                model[3].X = p.X + 1; model[3].Y = p.Y;

                //[+]   [ ]
                //[ ][ ][ ]
                //   [ ]  
                model[4].X = p.X - 1; model[4].Y = p.Y - 1;

                //[ ]   [+]
                //[ ][ ][ ]
                //   [ ]  
                model[5].X = p.X + 1; model[5].Y = p.Y - 1;
                #endregion
            } else if (d == Direction.left) {
                #region left generate
                //   [+][ ]
                //[ ][ ]
                //   [ ][ ]
                model[0].X = p.X; model[0].Y = p.Y - 1;

                //   [ ][+]
                //[ ][ ]
                //   [ ][ ]
                model[1].X = p.X + 1; model[1].Y = p.Y - 1;

                //   [ ][ ]
                //[+][ ]
                //   [ ][ ] 
                model[2].X = p.X - 1; model[2].Y = p.Y;

                //   [ ][ ]
                //[ ][+]
                //   [ ][ ] 
                model[3].X = p.X; model[3].Y = p.Y;

                //   [ ][ ]
                //[ ][ ]
                //   [+][ ]
                model[4].X = p.X; model[4].Y = p.Y + 1;

                //   [ ][ ]
                //[ ][ ]
                //   [ ][+] 
                model[5].X = p.X + 1; model[5].Y = p.Y + 1;
                #endregion
            } else if (d == Direction.right) {
                #region right generate
                //[+][ ]   
                //   [ ][ ]
                //[ ][ ]
                model[0].X = p.X - 1; model[0].Y = p.Y - 1;

                //[ ][+]   
                //   [ ][ ]
                //[ ][ ]
                model[1].X = p.X; model[1].Y = p.Y - 1;

                //[ ][ ]   
                //   [+][ ]
                //[ ][ ]
                model[2].X = p.X; model[2].Y = p.Y;

                //[ ][ ]   
                //   [ ][+]
                //[ ][ ] 
                model[3].X = p.X + 1; model[3].Y = p.Y;

                //[ ][ ]   
                //   [ ][ ]
                //[+][ ]
                model[4].X = p.X - 1; model[4].Y = p.Y + 1;

                //[ ][ ]   
                //   [ ][ ]
                //[ ][+]
                model[5].X = p.X; model[5].Y = p.Y + 1;
                #endregion
            }
        }

        private ConsoleColor color;
        public ConsoleColor Color { get { return color; } set { color = value; } }

        // Для отрисовки символа
        private char label;
        public char Label { get { return label; } set { label = value; } }

        private Point2D[] model;
        public Point2D[] Model { get { return model; } set { model = value; } }
    }
}
