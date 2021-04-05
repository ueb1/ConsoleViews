using BestiLeikurinn.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Display
{
    public class DisplayScreen
    {
        public int Columns { get; private set; }
        public int Lines { get; private set; }

        public List<DisplayBox> DisplayBoxes { get; set; }

        public DisplayScreen(int nrCols, int nrLines)
        {
            Columns = nrCols;
            Lines = nrLines;
            DisplayBoxes = new List<DisplayBox>();
            Console.SetWindowSize(nrCols, nrLines);
            Console.CursorVisible = false;
        }

        public void Add(DisplayBox box)
        {
            if (DisplayBoxes.Exists(x => x.Name == box.Name))
            {
                throw new DisplayBoxExistsException(box.Name);
            }
            else
                DisplayBoxes.Add(box);
        }

        public void PrintScreen(bool clear = false)
        {
            if (clear)
                Console.Clear();

            foreach(DisplayBox box in DisplayBoxes)
            {
                box.Print();
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 4);
        }
    }
}
