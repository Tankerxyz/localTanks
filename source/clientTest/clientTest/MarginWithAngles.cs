using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class MarginWithAngles: MarginIcons {
        public MarginWithAngles(            
            char upLeft = '╔',
            char upRight = '╗',
            char downRight = '╝',
            char downLeft = '╚',
            char up = '═',
            char down = '═',
            char left = '║',
            char right = '║')
            : base(up, down, left, right) {
            this.upLeft = upLeft;
            this.upRight = upRight;
            this.downRight = downRight;
            this.downLeft = downLeft;
        }

        private char upLeft;
        public char UpLeft { get { return upLeft; } set { upLeft = value; } }

        private char upRight;
        public char UpRight { get { return upRight; } set { upRight = value; } }

        private char downRight;
        public char DownRight { get { return downRight; } set { downRight = value; } }

        private char downLeft;
        public char DownLeft { get { return downLeft; } set { downLeft = value; } }
    }
}
