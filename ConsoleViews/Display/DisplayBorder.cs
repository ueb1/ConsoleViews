using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleViews.Display
{
    public class DisplayBorder
    {
        public const int LEFT = 0;
        public const int TOP = 1;
        public const int RIGHT = 2;
        public const int BOTTOM = 3;

        public char Symbol { get; set; }
        public int[] Thickness
        {
            get { return thickness; }
            private set
            {
                if (value[0] < 0)
                    throw new ArgumentException("Border thickness cannot be negative", "[0]");
                if (value[1] < 0)
                    throw new ArgumentException("Border thickness cannot be negative", "[1]");
                if (value[2] < 0)
                    throw new ArgumentException("Border thickness cannot be negative", "[2]");
                if (value[3] < 0)
                    throw new ArgumentException("Border thickness cannot be negative", "[3]");
                if (value[0] > 127)
                    throw new ArgumentException("Border thickness must me less than 128", "[0]");
                if (value[1] > 127)
                    throw new ArgumentException("Border thickness must me less than 128", "[1]");
                if (value[2] > 127)
                    throw new ArgumentException("Border thickness must me less than 128", "[2]");
                if (value[3] > 127)
                    throw new ArgumentException("Border thickness must me less than 128", "[3]");
                thickness = value;
            }
        }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        private int[] thickness;

        public DisplayBorder(char symbol)
        {
            Symbol = symbol;
            Thickness = new int[] { 0, 0, 0, 0};
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }

        public DisplayBorder(char symbol, int thickness, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Symbol = symbol;
            Thickness = new int[] { thickness, thickness, thickness, thickness };
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public DisplayBorder(char symbol, int thickness_left, int thickness_top, int thickness_right, int thickness_bottom, 
            ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Symbol = symbol;
            Thickness = new int[] { thickness_left, thickness_top, thickness_right, thickness_bottom };
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}
