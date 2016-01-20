using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    abstract class Window {
        abstract public void show();
        abstract public void clear();

        protected Point2D position;
        public Point2D Position { get { return position; } set { position = value; } }

        protected int width;
        public int Width { get { return width; } set { width = value; } }

        protected int height;
        public int Height { get { return height; } set { height = value; } }

        protected ConsoleColor foregroundColor;
        public ConsoleColor ForegroundColor { get { return foregroundColor; } set { foregroundColor = value; } }

        protected ConsoleColor backgroundColor;
        public ConsoleColor BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } }

        protected MarginWithAngles margin;
        public MarginWithAngles Margin { get { return margin; } set { margin = value; } }

        protected bool changed;
        public bool Changed { get { return changed; } set { changed = value; } }
    }
}
