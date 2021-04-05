using BestiLeikurinn.Cells;
using BestiLeikurinn.Display;
using BestiLeikurinn.Display.Entities;
using BestiLeikurinn.Enums;
using System;
using System.Collections.Generic;

namespace BestiLeikurinn
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayBorder mainBorder = new DisplayBorder('#', 1, ConsoleColor.Cyan);
            mainBorder.Thickness = new int[] { 1, 1, 0, 0 };
            DisplayBorder sideBorder = new DisplayBorder('#', 1, ConsoleColor.Red);
            sideBorder.Thickness = new int[] { 1, 1, 1, 0 };
            DisplayBorder bottomBorder = new DisplayBorder('#', 1, ConsoleColor.Yellow);
            DisplayBox mainBox = new DisplayBox("MainBox", 1, 1, 80, 30, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Black, mainBorder);
            DisplayBox sideBox = new DisplayBox("SideBox", mainBox.Width + 2, 1, 30, 30, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black, sideBorder);
            DisplayBox bottomBox = new DisplayBox("BottomBox", 1, mainBox.Height + 2, 80 + sideBox.Width + 1, 10, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Black, bottomBorder);
            mainBox.WriteText("Main", HorizontalAlignment.Center, VerticalAlignment.Center, ConsoleColor.White, ConsoleColor.Black);
            mainBox.WriteText("Main", HorizontalAlignment.Center, VerticalAlignment.Top, ConsoleColor.Yellow, ConsoleColor.Black);
            sideBox.WriteText("Side", HorizontalAlignment.Center, VerticalAlignment.Center, ConsoleColor.White, ConsoleColor.Black);
            bottomBox.WriteText("Bottom", HorizontalAlignment.Center, VerticalAlignment.Center, ConsoleColor.White, ConsoleColor.Black);
            DisplayScreen screen = new DisplayScreen(150, 50);
            screen.Add(mainBox);
            screen.Add(sideBox);
            screen.Add(bottomBox);
            screen.PrintScreen();
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            //Console.WriteLine(Console.OutputEncoding.EncodingName);
            //map[85, 20] = new Cell { BackgroundColor = ConsoleColor.Black, ForegroundColor = ConsoleColor.Green, Label = '@' };

            
            Console.ReadKey();
        }
    }
}
