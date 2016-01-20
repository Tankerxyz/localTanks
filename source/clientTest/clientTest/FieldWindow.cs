using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class FieldWindow: Window {
        public FieldWindow(
            int width,
            int height,
            Point2D position,
            char backgroundIcon = ' ',
            ConsoleColor foregroundColor = ConsoleColor.DarkGray,
            ConsoleColor backgroundColor = ConsoleColor.Black
            ) {

            this.width = width;
            this.height = height;
            this.position = position;
            this.backgroundIcon = backgroundIcon;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.margin = new MarginWithAngles();
            this.changed = true;
        }

        public FieldWindow(
            int width,
            int height,
            Point2D position,
            MarginWithAngles margin,
            char backgroundIcon = ' ',
            ConsoleColor foregroundColor = ConsoleColor.DarkGray,
            ConsoleColor backgroundColor = ConsoleColor.Black
            ) {

            this.width = width;
            this.height = height;
            this.position = position;
            this.margin = margin;
            this.backgroundIcon = backgroundIcon;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.changed = true;
        }

        public override void show() {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = foregroundColor;
            Console.SetCursorPosition(Position.X, Position.Y);
            for (int i = 0; i <= height; ++i) {
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
                        Console.BackgroundColor = backgroundColor;
                        Console.Write(backgroundIcon);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.WriteLine();
            }
        }

        public override void clear() {
            Console.BackgroundColor = Program.backgroundColor;
            Console.SetCursorPosition(Position.X, Position.Y);
            for (int i = 0; i <= height; ++i) {
                for (int j = 0; j <= width; ++j) {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private char backgroundIcon;
        public char BackgroundIcon { get { return backgroundIcon; } set { backgroundIcon = value; } }
    }
}
