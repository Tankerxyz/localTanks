using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class PixelNumbers3x5 {

        static public void show(Point2D p, ConsoleColor color, int numb) {
            Console.BackgroundColor = color;
            if (numb == 1) {
                for (int i = 0; i < 5; ++i) {
                    Console.SetCursorPosition(p.X + 2, p.Y + i);
                    Console.Write(" ");
                }
            } else if (numb == 2) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;

                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                ++p.Y;
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            } else if (numb == 3) {
                for (int count = 0; count < 2; ++count) {
                    for (int i = 0; i < 3; ++i) {
                        Console.SetCursorPosition(p.X + i, p.Y);
                        Console.Write(" ");
                    }
                    ++p.Y;

                    Console.SetCursorPosition(p.X + 2, p.Y);
                    Console.Write(" ");

                    ++p.Y;
                }
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            } else if (numb == 4) {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                p.Y++;
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                p.Y++;
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                p.Y++;
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                p.Y++;
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
            } else if (numb == 5) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                ++p.Y;

                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            } else if (numb == 6) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                ++p.Y;

                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            } else if (numb == 7) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                p.Y++;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                p.Y++;
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                p.Y++;
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                p.Y++;
                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
            } else if (numb == 8) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;

                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            } else if (numb == 9) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;

                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");
                ++p.Y;
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            } else if (numb == 0) {
                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;

                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");

                Console.SetCursorPosition(p.X + 2, p.Y);
                Console.Write(" ");

                ++p.Y;

                for (int i = 0; i < 3; ++i) {
                    Console.SetCursorPosition(p.X + i, p.Y);
                    Console.Write(" ");
                }
            }

            Console.ResetColor();
        }
    }
}
