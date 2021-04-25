using ConsoleViews.Display.Entities;
using ConsoleViews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleViews.Display
{
    public class DisplayScreen
    {
        public int Columns { get; private set; }
        public int Lines { get; private set; }

        private ColoredChar[,] buffer;
        private List<DisplayBox> displayBoxes;

        public DisplayScreen(int nrCols, int nrLines)
        {
            if (nrCols < 0)
                throw new ArgumentException("Value cannot be less than zero", "nrCols");
            if (nrLines < 0)
                throw new ArgumentException("Value cannot be less than zero", "nrLines");
            if (nrLines > 50)
                throw new ArgumentException("Max value is 50", "nrLines");
            if (nrCols > 150)
                throw new ArgumentException("Max value is 150", "nrCols");

            Columns = nrCols;
            Lines = nrLines;
            displayBoxes = new List<DisplayBox>();
            //Console.SetWindowSize(nrCols, nrLines + 2);
//            Console.CursorVisible = false;
            buffer = new ColoredChar[nrCols, nrLines];
        }

        public void Add(DisplayBox box)
        {
            if (displayBoxes.Exists(x => x.Name == box.Name))
            {
                throw new DisplayBoxExistsException(box.Name);
            }
            else
            {
                foreach(DisplayBox boxInList in displayBoxes)
                {
                    Rect intersection = Rect.Intersect(box.Rectangle, boxInList.Rectangle);
                    if (intersection.Width > 0 && intersection.Height > 0)
                        throw new DisplayBoxOverlapException();
                }
            }
            displayBoxes.Add(box);
        }

        public List<DisplayBox> GetDisplayBoxes()
        {
            return displayBoxes;
        }

        public void LoadBorders()
        {
            foreach (DisplayBox box in displayBoxes)
            {
                if (box.Border != null)
                {
                    for (int i = box.DisplayY; i < box.DisplayY + box.Border.Thickness[DisplayBorder.TOP] + box.Border.Thickness[DisplayBorder.BOTTOM]; i++)
                    {
                        int line = i;
                        if (i >= box.DisplayY + box.Border.Thickness[DisplayBorder.TOP])
                            line = (i - box.Border.Thickness[DisplayBorder.TOP]) + (box.DisplayHeight - box.Border.Thickness[DisplayBorder.BOTTOM]);
                        for (int j = box.DisplayX; j < box.DisplayX + box.DisplayWidth; j++)
                        {
                            buffer[j, line] = new ColoredChar { BackgroundColor = box.Border.BackgroundColor, Character = box.Border.Symbol, ForegroundColor = box.Border.ForegroundColor };
                        }
                    }

                    for (int i = box.DisplayY + box.Border.Thickness[DisplayBorder.TOP]; i < box.DisplayY + box.DisplayHeight - box.Border.Thickness[DisplayBorder.BOTTOM]; i++)
                    {
                        for (int j = box.DisplayX; j < box.DisplayX + box.Border.Thickness[DisplayBorder.LEFT]; j++)
                        {
                            buffer[j, i] = new ColoredChar { BackgroundColor = box.Border.BackgroundColor, Character = box.Border.Symbol, ForegroundColor = box.Border.ForegroundColor };
                        }

                        for (int j = box.DisplayX + box.DisplayWidth - box.Border.Thickness[DisplayBorder.RIGHT]; j < box.DisplayX + box.DisplayWidth; j++)
                        {
                            buffer[j, i] = new ColoredChar { BackgroundColor = box.Border.BackgroundColor, Character = box.Border.Symbol, ForegroundColor = box.Border.ForegroundColor };
                        }
                    }
                }
            }
        }

        public void WriteString(string boxName, string str, ConsoleColor foregroundColor, ConsoleColor backgroundColor, int line, int col)
        {
            DisplayBox box = displayBoxes.FirstOrDefault(x => x.Name == boxName);
            if (box != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if(col + i < box.DisplayWidth)
                        buffer[box.DisplayX + col + i, box.DisplayY + line] = new ColoredChar { BackgroundColor = backgroundColor, Character = str[i], ForegroundColor = foregroundColor };
                }
            }
            else
            {
                throw new ArgumentException("Display box with given name does not exists");
            }
        }
        
        public void Clear()
        {
            buffer = new ColoredChar[Columns, Lines];
        }

        public void Clear(string boxName)
        {
            DisplayBox box = displayBoxes.FirstOrDefault(x => x.Name == boxName);
            if (box != null)
            {
                for (int i = box.DisplayX; i < box.DisplayX + box.DisplayWidth; i++)
                {
                    for (int j = box.DisplayY; j < box.DisplayY + box.DisplayHeight; j++)
                    {
                        buffer[i, j] = new ColoredChar();
                    }
                }
            }
        }

        public void PrintScreen()
        {
            int writes = 0;
            for (int i = 0; i < buffer.GetLength(1); i++)
            {
                string stringBuffer = "";
                for (int j = 0; j < buffer.GetLength(0); j++)
                {
                    if (buffer[j, i].Character != 0)
                    {
                        if (buffer[j, i].ForegroundColor != Console.ForegroundColor || buffer[j, i].BackgroundColor != Console.BackgroundColor)
                        {
                            if (stringBuffer.Length > 0)
                            {
                                Console.Write(stringBuffer);
                                stringBuffer = "";
                            }
                            Console.ForegroundColor = buffer[j, i].ForegroundColor;
                            Console.BackgroundColor = buffer[j, i].BackgroundColor;
                        }
                        stringBuffer += buffer[j, i].Character;
                    }
                    else
                    {
                        // Optimized for empty cells
                        Console.BackgroundColor = ConsoleColor.Black;
                        while (j < Columns && buffer[j, i].Character == 0)
                        {
                            j++;
                            stringBuffer += ' ';
                        }
                        j--;
                    }
                };
                Console.WriteLine(stringBuffer);
            }

        }
    }
}
