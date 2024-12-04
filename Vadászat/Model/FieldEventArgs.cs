using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vadaszat.Model
{
    public class FieldEventArgs : EventArgs
    {
        private int x;
        private int y;
        private Color color;

        public Int32 X { get { return x; } }
        public Int32 Y { get { return y; } }

        public Color Color { get { return color; } }

        public FieldEventArgs(int xx, int yy, Color g)
        {
            this.x = xx;
            this.y = yy;
            this.color = g;
        }
    }
    public class OverEventArgs : EventArgs
    {
        private int nyer;
        public int nyertes()
        {
            return nyer;
        }
        public OverEventArgs(int a)
        {
            nyer = a;
        }
    }
}
