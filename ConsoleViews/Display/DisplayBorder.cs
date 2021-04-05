using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Display
{
    public class DisplayBorder
    {
        public const int LEFT = 0;
        public const int TOP = 1;
        public const int RIGHT = 2;
        public const int BOTTOM = 3;

        public char Symbol { get; set; }
        public int[] Thickness{ get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

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
    }
}
