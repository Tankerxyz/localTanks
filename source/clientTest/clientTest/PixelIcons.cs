using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class PixelIcons5x4 {

        static public void showHeart(Point2D p, ConsoleColor color) {
            Console.BackgroundColor = color;

            Console.SetCursorPosition(p.X + 1, p.Y);
            Console.Write(" ");
            Console.SetCursorPosition(p.X + 3, p.Y);
            Console.Write(" ");
            p.Y++;

            for (int i = 0; i < 5; ++i) {
                Console.SetCursorPosition(p.X + i, p.Y);
                Console.Write(" ");
            }
            p.Y++;
            for (int i = 0; i < 3; ++i) {
                Console.SetCursorPosition((p.X + 1) + i, p.Y);
                Console.Write(" ");
            }
            p.Y++;
            Console.SetCursorPosition(p.X + 2, p.Y);
            Console.Write(" ");

            Console.ResetColor();
        }

        static public void showSword(Point2D p, ConsoleColor colorHandle, ConsoleColor colorCenter, ConsoleColor colorBlade) {
            Console.BackgroundColor = colorBlade;

            Console.SetCursorPosition(p.X + 2, p.Y);
            Console.Write(" ");
            p.Y++;
            Console.SetCursorPosition(p.X + 2, p.Y);
            Console.Write(" ");
            p.Y++;

            Console.BackgroundColor = colorCenter;

            for (int i = 0; i < 3; ++i) {
                Console.SetCursorPosition((p.X + 1) + i, p.Y);
                Console.Write(" ");
            }
            p.Y++;

            Console.BackgroundColor = colorHandle;
            Console.SetCursorPosition(p.X + 2, p.Y);
            Console.Write(" ");

            Console.ResetColor();
        }

        static public void showSpeed(Point2D p, ConsoleColor colorMargins, ConsoleColor colorCursor, ConsoleColor colorBack) {
            Console.BackgroundColor = colorMargins;

            for (int i = 0; i < 3; ++i) {
                Console.SetCursorPosition((p.X + 1) + i, p.Y);
                Console.Write(" ");
            }
            p.Y++;
            Console.SetCursorPosition(p.X, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorBack;

            Console.SetCursorPosition(p.X + 1, p.Y);
            Console.Write(" ");

            Console.SetCursorPosition(p.X + 2, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorCursor;

            Console.SetCursorPosition(p.X + 3, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorMargins;

            Console.SetCursorPosition(p.X + 4, p.Y);
            Console.Write(" ");
            p.Y++;

            Console.SetCursorPosition(p.X, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorBack;

            Console.SetCursorPosition(p.X + 1, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorCursor;

            Console.SetCursorPosition(p.X + 2, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorBack;

            Console.SetCursorPosition(p.X + 3, p.Y);
            Console.Write(" ");

            Console.BackgroundColor = colorMargins;

            Console.SetCursorPosition(p.X + 4, p.Y);
            Console.Write(" ");
            p.Y++;
            for (int i = 0; i < 3; ++i) {
                Console.SetCursorPosition((p.X + 1) + i, p.Y);
                Console.Write(" ");
            }

            Console.ResetColor();
        }
    }
}
