using ConsoleViews.Cells;
using ConsoleViews.Display;
using ConsoleViews.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ConsoleViews
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayBorder mainBorder = new DisplayBorder('#', 1, ConsoleColor.Cyan);
            mainBorder.Thickness = new int[] { 1, 1, 1, 1 };
            DisplayBorder sideBorder = new DisplayBorder('#', 1, ConsoleColor.Red);
            sideBorder.Thickness = new int[] { 1, 1, 1, 1 };
            DisplayBorder bottomBorder = new DisplayBorder('#', 1, ConsoleColor.Yellow);
            DisplayBox mainBox = new DisplayBox("MainBox", 0, 0, 80, 30, mainBorder);
            DisplayBox sideBox = new DisplayBox("SideBox", mainBox.DisplayWidth, 0, 30, 30, sideBorder);
            DisplayBox bottomBox = new DisplayBox("BottomBox", 0, mainBox.DisplayHeight, mainBox.DisplayWidth + sideBox.DisplayWidth , 10, bottomBorder);
            DisplayScreen screen = new DisplayScreen(150, 50);
            screen.Add(mainBox);
            screen.Add(sideBox);
            screen.Add(bottomBox);
            screen.LoadBorders();

            screen.WriteString("MainBox", "Hello World", ConsoleColor.White, ConsoleColor.Black, 15, 35);
            screen.WriteString("MainBox", "How are you today?", ConsoleColor.Red, ConsoleColor.Black, 16, 31);
            //screen.WriteString("SideBox", "Side box", ConsoleColor.White, ConsoleColor.Black, 15, 12);
            screen.WriteString("SideBox", "Welcome ", ConsoleColor.White, ConsoleColor.Black, 2, 2);
            screen.WriteString("SideBox", "Unnar", ConsoleColor.Yellow, ConsoleColor.Black, 2, 10);
            screen.WriteString("SideBox", "Today is: ", ConsoleColor.White, ConsoleColor.Black, 4, 2);
            screen.WriteString("SideBox", "Monday", ConsoleColor.Yellow, ConsoleColor.Black, 4, 12);
            screen.WriteString("SideBox", "New messages: ", ConsoleColor.White, ConsoleColor.Black, 6, 2);
            screen.WriteString("SideBox", "2", ConsoleColor.Yellow, ConsoleColor.Black, 6, 16);
            screen.WriteString("BottomBox", " 21.3.2021 14:23 - Some event has been logged", ConsoleColor.White, ConsoleColor.Black, 2, 1);
            screen.WriteString("BottomBox", " 21.3.2021 14:42 - Some event has been logged", ConsoleColor.White, ConsoleColor.Black, 3, 1);
            screen.WriteString("BottomBox", " 21.3.2021 17:42 - Some event has been logged", ConsoleColor.White, ConsoleColor.Black, 4, 1);
            //StringWriter sw = new StringWriter();
            //Console.SetOut(sw);
            screen.PrintScreen();
            Console.CursorVisible = true;
            Console.ReadKey();
        }
    }
}
