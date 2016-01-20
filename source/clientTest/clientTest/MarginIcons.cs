using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientTest {
    class MarginIcons {

        public MarginIcons(char up = '═', char down = '═', char left = '║', char right = '║') {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        private char up;
        public char Up { get { return up; } set { up = value; } }

        private char down;
        public char Down { get { return down; } set { down = value; } }

        private char left;
        public char Left { get { return left; } set { left = value; } }

        private char right;
        public char Right { get { return right; } set { right = value; } }
    }
}
