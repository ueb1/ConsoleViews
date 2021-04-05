using BestiLeikurinn.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Display.Entities
{
    public class ConsoleString
    {
        public ConsoleColor TextColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public string Text { get; set; }
        public int Length 
        { 
            get { return Text.Length; } 
        }
    }
}
