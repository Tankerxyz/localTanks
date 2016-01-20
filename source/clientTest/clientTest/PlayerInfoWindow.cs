using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class PlayerInfoWindow: Window {

        public PlayerInfoWindow(
            int width,
            int height,
            Point2D position,
            ConsoleColor foregroundColor = ConsoleColor.DarkGray,
            ConsoleColor backgroundColor = ConsoleColor.Black
            ) {

            this.width = width;
            this.height = height;
            this.position = position;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.margin = new MarginWithAngles();
            this.changed = true;
        }

        public override void show() {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            for (int i = 0; i <= height; ++i) {
                Console.SetCursorPosition(Position.X, Position.Y + i);
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
                        Console.Write(" ");
                    }
                }
                if (i != height) {
                    Console.WriteLine();
                }
            }
            PixelIcons5x4.showHeart(new Point2D(position.X + 1, position.Y + 2), ConsoleColor.Red);
            PixelNumbers3x5.show(new Point2D(position.X + 2, position.Y + 7), ConsoleColor.White, Program.myPlayer.Stat.Health);

            PixelIcons5x4.showSword(new Point2D(position.X + 7, position.Y + 2), ConsoleColor.DarkYellow, ConsoleColor.DarkGray, ConsoleColor.Gray);
            PixelNumbers3x5.show(new Point2D(position.X + 7, position.Y + 7), ConsoleColor.White, Program.myPlayer.Bullet.Damage);

            PixelIcons5x4.showSpeed(new Point2D(position.X + 13, position.Y + 2), ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.White);
            PixelNumbers3x5.show(new Point2D(position.X + 13, position.Y + 7), ConsoleColor.White, Program.myPlayer.Speed);

        }

        public override void clear() {
            throw new NotImplementedException();
        }
    }
}
