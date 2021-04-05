using BestiLeikurinn.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Display
{
    public interface IDisplayBox
    {
        void WriteText(string newString, HorizontalAlignment hAlignment, VerticalAlignment vAlignment, ConsoleColor textColor, ConsoleColor backgroundColor, bool newline = true);
        void Print();
        void Clear();
    }
}
