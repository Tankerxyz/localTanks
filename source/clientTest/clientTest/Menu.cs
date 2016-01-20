using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest
{
    delegate void method();
    class Menu
    {

        private List<string> button;
        public List<string> Button { get { return button; } set { button = value; } }

        private List<method> arrMethods;
        public List<method> ArrMethods { get { return arrMethods; } set { arrMethods = value; } }

        private int cursor;
        public int Cursor 
        {
            get 
            {
                return cursor; 
            }
            set
            {
                cursor = (value >= 0 && value < button.Count) ? value : cursor;
            }
        }
        
        private ConsoleColor highLightColor;
        public ConsoleColor HighLightColor { get { return highLightColor; } set { highLightColor = value; } }

        private ConsoleColor textColor;
        public ConsoleColor TextColor { get { return textColor; } set { textColor = value; } }

        private Point2D startPos;
        public Point2D StartPos { get { return startPos; } set { startPos = value; } }

        public Menu(ConsoleColor highLightColor, ConsoleColor textColor, Point2D startPos, int cursor, params string[] button)
        {
            this.button = new List<string>();
            for (int i = 0; i < button.Length; ++i)
            {
                this.button.Add(button[i]);
            }

            this.highLightColor = highLightColor;
            this.textColor = textColor;
            this.startPos = startPos;
            this.Cursor = cursor;
        }

        public Menu(List<string> button, ConsoleColor highLightColor, ConsoleColor textColor, Point2D startPos, int cursor = 0)
        {
            this.button = button;
            this.highLightColor = highLightColor;
            this.textColor = textColor;
            this.startPos = startPos;
            this.Cursor = cursor;
        }

        public Menu(List<string> button,
            List<method> arrMethods,
            Point2D startPos,
            ConsoleColor highLightColor,
            ConsoleColor textColor,
            int cursor = 0,
            bool whritable = false)
        {
            this.button = button;
            this.arrMethods = arrMethods;
            this.highLightColor = highLightColor;
            this.textColor = textColor;
            this.startPos = startPos;
            this.cursor = cursor;
        }

        public int run()
        {
            while (true) {
                Console.Clear();
                this.show();

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key) {
                    case ConsoleKey.DownArrow:
                        this.Cursor++;
                        break;
                    case ConsoleKey.UpArrow:
                        this.Cursor--;
                        break;
                    case ConsoleKey.Escape:
                        return -1;
                    case ConsoleKey.Enter:
                        return cursor;
                }
            }
            
        }

        public void show()
        {
            //Console.Clear();
            //Сохраняю начальный цвет консоли для предотвращения конфликта, связанного с отображением всего окна - цветом меню.
            ConsoleColor beforeHighLightColor = Console.BackgroundColor;
            ConsoleColor beforeTextColor = Console.ForegroundColor;

            //Меняю цвет консоли            
            Console.ForegroundColor = textColor;

            int tempStartPosY = startPos.Y;

            int x = 0, y = 0;
            for (int i = 0; i < button.Count; ++i)
            {
                //Устанавливаю позицию последующим увеличением с шагом в 2
                Console.SetCursorPosition(startPos.X, startPos.Y + i*2);

                //Устанавливаю выделение
                if(cursor == i)
                    Console.BackgroundColor = highLightColor;

                Console.Write(button[i]);

                //Убираю выделение
                if (cursor == i)
                {
                    Console.BackgroundColor = beforeHighLightColor;
                    x = Console.CursorLeft;
                    y = Console.CursorTop;
                }
            }

            Console.CursorLeft = x;
            Console.CursorTop = y; 


            //Сброс цвета консоли до заводских
            Console.BackgroundColor = beforeHighLightColor;
            Console.ForegroundColor = beforeTextColor;
        }
    }
}
