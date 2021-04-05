using BestiLeikurinn.Cells;
using BestiLeikurinn.Display.Entities;
using BestiLeikurinn.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Display
{
    public class DisplayBox : IDisplayBox
    {
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int cursorX { get; private set; }
        public int cursorY { get; private set; }
        public List<List<ConsoleString>> Screen { get; private set; }
        public ConsoleColor DefaultTextForegroundColor { get; private set; }
        public ConsoleColor DefaultTextBackgroundColor { get; private set; }
        public ConsoleColor BackgroundColor { get; private set; }
        public DisplayBorder Border { get; private set; }
        public Cell[,] Display;

        public string[] buffer;

        public DisplayBox(string name)
        {

        }

        public DisplayBox(string name, int x, int y, int width, int height,
            ConsoleColor defaultTextForegroundColor = ConsoleColor.White,
            ConsoleColor defaultTextBackgroundColor = ConsoleColor.Black,
            ConsoleColor backgroundColor = ConsoleColor.Black,
            DisplayBorder border = null)
        {
            Screen = new List<List<ConsoleString>>();
            for (int i = 0; i < height; i++)
            {
                Screen.Add(new List<ConsoleString>());
            }
            Name = name;
            Width = width;
            Height = height;
            cursorX = 0;
            cursorY = 0;
            X = x;
            Y = y;
            Display = new Cell[width, height];
            Border = border;
            DefaultTextBackgroundColor = defaultTextBackgroundColor;
            DefaultTextForegroundColor = defaultTextForegroundColor;
            BackgroundColor = backgroundColor;
            buffer = new string[height + border.Thickness[DisplayBorder.TOP] + border.Thickness[DisplayBorder.BOTTOM]];
            Clear();
        }

        public void WriteCell(int x, int y, Cell cell)
        {
            if(x >= 0 && x < Width && y >= 0 && y < Height)
                Display[x, y] = cell;
        }

        public void WriteText(
            string newString,
            HorizontalAlignment hAlignment,
            VerticalAlignment vAlignment,
            ConsoleColor textColor,
            ConsoleColor backgroundColor,
            bool newline = true)
        {
            switch (vAlignment)
            {
                case VerticalAlignment.Top:
                    cursorY = 0;
                    break;
                case VerticalAlignment.Center:
                    cursorY = Height / 2;
                    break;
                case VerticalAlignment.Bottom:
                    cursorY = Height;
                    break;
            }

            switch (hAlignment)
            {
                case HorizontalAlignment.Left:
                    cursorX = 0;
                    break;
                case HorizontalAlignment.Center:
                    cursorX = (Width - newString.Length) / 2;
                    break;
                case HorizontalAlignment.Right:
                    cursorX = Width - newString.Length;
                    break;
            }

            int[] headStringInfo = GetStringInfoAt(cursorX, cursorY);
            int[] tailStringInfo = GetStringInfoAt(cursorX + newString.Length, cursorY);
            if (headStringInfo == null)
            {
                int preStringLength = Screen[cursorY].Sum(x => x.Length);
                Screen[cursorY].Add(new ConsoleString { Text = new string(' ', cursorX - preStringLength) });
                Screen[cursorY].Add(new ConsoleString { BackgroundColor = backgroundColor, Text = newString, TextColor = textColor });
            }
            else
            {
                // Split the leading string
                int split = cursorX - headStringInfo[1]; // Where to split the string;
                ConsoleString head = Screen[cursorY][headStringInfo[0]];
                ConsoleString tail = Screen[cursorY][tailStringInfo[0]];
                string originalString = head.Text;
                head.Text = head.Text.Substring(0, split);

                if (tailStringInfo[0] < 0)
                {
                    Screen[cursorY].Add(new ConsoleString { BackgroundColor = backgroundColor, Text = newString, TextColor = textColor });
                }
                else if (head == tail)
                {
                    if (originalString.Length > split + newString.Length)
                    {
                        ConsoleString newTail = new ConsoleString
                        {
                            BackgroundColor = head.BackgroundColor,
                            Text = originalString.Substring(split + newString.Length),
                            TextColor = head.TextColor
                        };
                        Screen[cursorY].Insert(headStringInfo[0] + 1, new ConsoleString { BackgroundColor = backgroundColor, Text = newString, TextColor = textColor });
                        Screen[cursorY].Insert(headStringInfo[0] + 2, newTail);
                    }
                    else
                        Screen[cursorY].Insert(headStringInfo[0] + 1, new ConsoleString { BackgroundColor = backgroundColor, Text = newString, TextColor = textColor });

                }
                else
                {
                    split = (cursorX + newString.Length) - tailStringInfo[1];
                    tail.Text = tail.Text.Substring(split);
                    if (tail.Text.Length == 0)
                        Screen[cursorY].RemoveAt(tailStringInfo[0]);

                    Screen[cursorY].RemoveRange(headStringInfo[0] + 1, tailStringInfo[0] - (headStringInfo[0] + 1));
                    Screen[cursorY].Insert(headStringInfo[0] + 1, new ConsoleString { BackgroundColor = backgroundColor, Text = newString, TextColor = textColor });
                }
            }

            cursorX += newString.Length;
            if (newline)
                cursorY++;
        }
        public void Print()
        {
            if(Border != null)
            {
                int borderX = X - Border.Thickness[DisplayBorder.LEFT];
                int borderY = Y - Border.Thickness[DisplayBorder.TOP];
                int adjustmentX = 0;
                int adjustmentY = 0;
                if (borderX < 0)
                {
                    adjustmentX -= borderX;
                    borderX = 0;
                }
                if(borderY < 0)
                {
                    adjustmentY -= borderY;
                    borderY = 0;
                }
                Console.ForegroundColor = Border.ForegroundColor;
                Console.BackgroundColor = Border.BackgroundColor;
                Console.SetCursorPosition(borderX, borderY);
                for (int i = 0; i < Border.Thickness[DisplayBorder.TOP] - adjustmentY; i++)
                {
                    Console.Write(new string(Border.Symbol, Width + Border.Thickness[DisplayBorder.LEFT] + Border.Thickness[DisplayBorder.RIGHT] - adjustmentX));
                    Console.SetCursorPosition(borderX, Console.CursorTop + 1);
                }
                for (int i = 0; i < Height; i++)
                {
                    Console.Write(new string(Border.Symbol, Border.Thickness[DisplayBorder.LEFT] - adjustmentX));
                    Console.SetCursorPosition(borderX + Border.Thickness[DisplayBorder.LEFT] + Width - adjustmentX, Console.CursorTop);
                    Console.Write(new string(Border.Symbol, Border.Thickness[DisplayBorder.RIGHT]));
                    Console.SetCursorPosition(borderX, Console.CursorTop + 1);
                }
                for (int i = 0; i < Border.Thickness[DisplayBorder.BOTTOM]; i++)
                {
                    Console.Write(new string(Border.Symbol, Width + Border.Thickness[DisplayBorder.LEFT] + Border.Thickness[DisplayBorder.RIGHT] - adjustmentX));
                    Console.SetCursorPosition(borderX, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(X, Y);
            //for (int i = 0; i < Display.GetLength(0); i++)
            //{
            //    for (int j = 0; j < Display.GetLength(1); j++)
            //    {
            //        if(Display[i, j] != null)
            //        {
            //            Console.SetCursorPosition(X + i, Y + j);
            //            Console.ForegroundColor = Display[i, j].ForegroundColor;
            //            Console.BackgroundColor = Display[i, j].BackgroundColor;
            //            Console.Write(Display[i, j].Label);
            //        }
            //    }
            //}
            foreach (List<ConsoleString> stringList in Screen)
            {
                foreach (ConsoleString consoleString in stringList)
                {
                    Console.BackgroundColor = consoleString.BackgroundColor;
                    Console.ForegroundColor = consoleString.TextColor;
                    Console.Write(consoleString.Text);
                }
                Console.SetCursorPosition(X, Console.CursorTop + 1);
            }
        }

        public void Clear()
        {
            //Screen = new List<List<ConsoleString>>();
            //for (int i = 0; i < Height; i++)
            //{
            //    Screen.Add(new List<ConsoleString>());
            //    if (BackgroundColor != Console.BackgroundColor)
            //        Screen[i].Add(new ConsoleString { BackgroundColor = BackgroundColor, Text = new string(' ', Width) });
            //}
            Display = new Cell[Width, Height];
            Console.BackgroundColor = BackgroundColor;
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.Write(new string(' ', Width));
            }
        }

        private ConsoleString GetStringAt(int cursorPositionX, int cursorPositionY)
        {
            int charCount = 0;
            for (int i = 0; i < Screen[cursorPositionY].Count; i++)
            {
                ConsoleString currentString = Screen[cursorPositionY][i];
                charCount += currentString.Length;
                if (cursorPositionX <= charCount)
                    return currentString;
            }
            return null;
        }

        private int[] GetStringInfoAt(int cursorPositionX, int cursorPositionY)
        {
            int currentStringScreenPosition = 0;
            for (int i = 0; i < Screen[cursorPositionY].Count; i++)
            {
                ConsoleString currentString = Screen[cursorPositionY][i];
                if (cursorPositionX < currentStringScreenPosition + currentString.Length)
                    return new int[] { i, currentStringScreenPosition };
                currentStringScreenPosition += currentString.Length;


            }
            return null;
        }
    }
}
