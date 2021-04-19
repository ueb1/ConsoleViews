using ConsoleViews.Cells;
using ConsoleViews.Display;
using ConsoleViews.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleViews
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayBorder mainBorder = new DisplayBorder('#', 1, ConsoleColor.Cyan);
            mainBorder.Thickness = new int[] { 100, 100, 100, 100 };
            DisplayBorder sideBorder = new DisplayBorder('#', 1, ConsoleColor.Red);
            sideBorder.Thickness = new int[] { 1, 1, 1, 1 };
            DisplayBorder bottomBorder = new DisplayBorder('#', 1, ConsoleColor.Yellow);
            DisplayBox mainBox = new DisplayBox("MainBox", 0, 0, 20, 10, mainBorder);
            DisplayBox sideBox = new DisplayBox("SideBox", mainBox.DisplayWidth, 0, 30, 30, sideBorder);
            DisplayBox bottomBox = new DisplayBox("BottomBox", 0, mainBox.DisplayHeight, mainBox.DisplayWidth + sideBox.DisplayWidth , 10, bottomBorder);
            DisplayScreen screen = new DisplayScreen(30, 20);
            screen.Add(mainBox);
            //screen.Add(sideBox);
            //screen.Add(bottomBox);
            screen.LoadBorders();

            screen.WriteString("MainBox", "Hello World", ConsoleColor.Red, ConsoleColor.Green, 15, 35);
            //screen.WriteString("SideBox", "Hello", ConsoleColor.Red, ConsoleColor.Black, 15, 12);
            //screen.WriteString("BottomBox", "Hello again", ConsoleColor.Red, ConsoleColor.Black, 5, 50);

            StringWriter sw = new StringWriter();
            //Console.SetOut(sw);
            screen.PrintScreen();
            Console.CursorVisible = true;
            Console.ReadKey();
        }
    }
}
