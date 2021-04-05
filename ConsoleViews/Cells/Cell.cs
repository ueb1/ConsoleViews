using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Cells
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public char Label { get; set; }
        public bool Changed { get; protected set; }

        public Cell(int x, int y, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char label)
        {
            X = x;
            Y = y;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            Label = label;
            Changed = true;
        }

        public void Printed()
        {
            Changed = false;
        }
    }
}
