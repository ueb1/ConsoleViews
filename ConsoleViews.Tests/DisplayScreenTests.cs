using ConsoleViews.Display;
using ConsoleViews.Enums;
using ConsoleViews.Exceptions;
using System;
using System.IO;
using Xunit;

namespace ConsoleViews.Tests
{
    public class DisplayScreenTests
    {
        [Theory]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 0, "####################")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 1, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 2, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 3, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 4, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 5, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 6, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 7, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 8, "#                  #")]
        [InlineData('#', 1, 1, 1, 1, 20, 10, 9, "####################")]
        // Double thickness
        [InlineData('#', 2, 2, 2, 2, 20, 10, 0, "####################")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 1, "####################")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 2, "##                ##")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 3, "##                ##")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 4, "##                ##")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 5, "##                ##")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 6, "##                ##")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 7, "##                ##")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 8, "####################")]
        [InlineData('#', 2, 2, 2, 2, 20, 10, 9, "####################")]
        // Varying thickness
        [InlineData('#', 1, 2, 4, 3, 20, 10, 0, "####################")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 1, "####################")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 2, "#               ####")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 3, "#               ####")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 4, "#               ####")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 5, "#               ####")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 6, "#               ####")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 7, "####################")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 8, "####################")]
        [InlineData('#', 1, 2, 4, 3, 20, 10, 9, "####################")]
        // Borders with zero thickness
        [InlineData('#', 0, 0, 0, 0, 20, 10, 0, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 1, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 2, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 3, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 4, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 5, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 6, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 7, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 8, "                    ")]
        [InlineData('#', 0, 0, 0, 0, 20, 10, 9, "                    ")]
        public void LoadBorders_BordersDisplayedCorrectly(char borderChar, int borderThickness_left, int borderThickness_top, int borderThickness_right, int borderThickness_bottom, int boxWidth, int boxHeight,
            int checkLine, string expectedString)
        {
            //Set up
            DisplayScreen screen = new DisplayScreen(150, 50);
            DisplayBorder border = new DisplayBorder(borderChar);
            border.Thickness[DisplayBorder.LEFT] = borderThickness_left;
            border.Thickness[DisplayBorder.TOP] = borderThickness_top;
            border.Thickness[DisplayBorder.RIGHT] = borderThickness_right;
            border.Thickness[DisplayBorder.BOTTOM] = borderThickness_bottom;
            DisplayBox box = new DisplayBox("MyBox", 0, 0, boxWidth, boxHeight, border);
            screen.Add(box);

            //Action
            screen.LoadBorders();
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            screen.PrintScreen();
            string targetString = sw.ToString().Substring(checkLine * 150 + (checkLine * 2), boxWidth);

            //Assert
            Assert.True(targetString == expectedString);
        }

        [Theory]
        //Too large
        [InlineData(11, 1, 1, 1, 20, 10)]
        [InlineData(1, 6, 1, 1, 20, 10)]
        [InlineData(1, 1, 11, 1, 20, 10)]
        [InlineData(1, 1, 1, 6, 20, 10)]
        //Negative thickness
        [InlineData(-1, 1, 1, 1, 20, 10)]
        [InlineData(1, -1, 1, 1, 20, 10)]
        [InlineData(1, 1, -1, 1, 20, 10)]
        [InlineData(1, 1, 1, -1, 20, 10)]
        public void LoadBorders_InvalidBordersThrowException(int borderThickness_left, int borderThickness_top, 
            int borderThickness_right, int borderThickness_bottom, int boxWidth, int boxHeight)
        {
            //Set up
            DisplayBorder border = new DisplayBorder('#');
            border.Thickness[DisplayBorder.LEFT] = borderThickness_left;
            border.Thickness[DisplayBorder.TOP] = borderThickness_top;
            border.Thickness[DisplayBorder.RIGHT] = borderThickness_right;
            border.Thickness[DisplayBorder.BOTTOM] = borderThickness_bottom;

            //Action - assert
            Assert.Throws<InvalidBorderException>(() => new DisplayBox("MyBox", 0, 0, boxWidth, boxHeight, border));
        }

        [Theory]
        //Upper bounds
        [InlineData(10, 1, 1, 1, 20, 10)]
        [InlineData(1, 5, 1, 1, 20, 10)]
        [InlineData(1, 1, 10, 1, 20, 10)]
        [InlineData(1, 1, 1, 5, 20, 10)]
        [InlineData(10, 5, 10, 5, 20, 10)]

        //Lower bounds
        [InlineData(0, 1, 1, 1, 20, 10)]
        [InlineData(1, 0, 1, 1, 20, 10)]
        [InlineData(1, 1, 0, 1, 20, 10)]
        [InlineData(1, 1, 1, 0, 20, 10)]
        [InlineData(0, 0, 0, 0, 20, 10)]
        public void LoadBorders_BordersAreValid(int borderThickness_left, int borderThickness_top, 
            int borderThickness_right, int borderThickness_bottom, int boxWidth, int boxHeight)
        {
            //Set up
            DisplayBorder border = new DisplayBorder('#');
            border.Thickness[DisplayBorder.LEFT] = borderThickness_left;
            border.Thickness[DisplayBorder.TOP] = borderThickness_top;
            border.Thickness[DisplayBorder.RIGHT] = borderThickness_right;
            border.Thickness[DisplayBorder.BOTTOM] = borderThickness_bottom;

            //Action
            new DisplayBox("MyBox", 0, 0, boxWidth, boxHeight, border);

            //No assert needed as test succeeds if no exception is thrown
        }

        [Fact]
        public void Add_BoxNameAlreadyExistsThrowsException()
        {
            //Set up
            string boxName = "MyBox";
            DisplayScreen screen = new DisplayScreen(150, 50);
            DisplayBox box1 = new DisplayBox(boxName, 0, 0, 80, 30);
            DisplayBox box2 = new DisplayBox(boxName, 0, 0, 80, 30);

            //Action
            screen.Add(box1);

            //Assert
            Assert.Throws<DisplayBoxExistsException>(() => screen.Add(box2));
        }

        [Fact]
        public void Add_NewBoxGetsAddedCorrectly()
        {
            //Set up
            string boxName = "MyBox";
            DisplayScreen screen = new DisplayScreen(150, 50);
            DisplayBox box = new DisplayBox(boxName, 0, 0, 80, 30);

            //Action
            screen.Add(box);
            string[] names = screen.GetDisplayBoxNames();

            //Assert
            Assert.True(names.Length > 0);
            Assert.True(names[0] == boxName);
        }

        [Theory]
        [InlineData(0, 0, 20, 10, 20, 0, 20, 10)]
        [InlineData(0, 0, 20, 10, 0, 10, 20, 10)]
        public void Add_NewBoxGetsAddedToNonEmptyList(int box1X, int box1Y, int box1Width, int box1Height, int box2X, int box2Y, int box2Width, int box2Height)
        {
            //Set up
            DisplayBox box1 = new DisplayBox("Box1", box1X, box1Y, box1Width, box1Height);
            DisplayBox box2 = new DisplayBox("Box2", box2X, box2Y, box2Width, box2Height);
            DisplayScreen screen = new DisplayScreen(150, 50);
            screen.Add(box1);

            //No assert needed as test succeeds if no exception is thrown
        }
        [Theory]
        [InlineData(0, 0, 20, 10, 19, 0, 20, 10)]
        [InlineData(0, 0, 20, 10, 0, 9, 20, 10)]
        public void Add_OverlappingBoxesThrowsException(int box1X, int box1Y, int box1Width, int box1Height, int box2X, int box2Y, int box2Width, int box2Height)
        {
            //Set up
            DisplayBox box1 = new DisplayBox("Box1", box1X, box1Y, box1Width, box1Height);
            DisplayBox box2 = new DisplayBox("Box2", box2X, box2Y, box2Width, box2Height);
            DisplayScreen screen = new DisplayScreen(150, 50);
            screen.Add(box1);

            //Assertion
            Assert.Throws<DisplayBoxOverlapException>(() => screen.Add(box2));
        }

        [Theory]
        [InlineData("Hello world!", 15, 35, "Hello world!")]
        [InlineData("Hello world!", 0, 0, "Hello world!")]
        [InlineData("Hello world!", 40, 68, "")]
        [InlineData("Hello world!", 0, 75, "Hello")]
        public void PrintScreen_TextDisplayedCorrectly(string text, int line, int column, string expectedString)
        {
            //Set up
            int displayWidth = 150;
            int displayHeight = 50;
            int boxX = 0;
            int boxY = 0;
            int boxWidth = 80;
            int boxHeight = 30;
            DisplayBox box = new DisplayBox("MyBox", boxX, boxY, boxWidth, boxHeight);
            DisplayScreen screen = new DisplayScreen(displayWidth, displayHeight);
            screen.Add(box);
            int stringBufferPtr = displayWidth * line + (line * 2) + column;

            //Action
            screen.WriteString("MyBox", text, ConsoleColor.White, ConsoleColor.Black, line, column);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            screen.PrintScreen();
            string targetString = sw.ToString().Substring(stringBufferPtr, expectedString.Length);

            //Assert
            Assert.True(targetString == expectedString);
        }

        [Fact]
        public void WriteString_InvalidBoxNameExceptionThrown()
        {
            //Set up
            DisplayBox box = new DisplayBox("MyBox", 0, 0, 80, 30);
            DisplayScreen screen = new DisplayScreen(150, 50);
            screen.Add(box);

            //Action - assert
            Assert.Throws<ArgumentException>(() => screen.WriteString("BogusName", "SomeText", ConsoleColor.Black, ConsoleColor.White, 0, 0));
        }

        //[Fact]
        //public void Clear_OutputGetsCleared()
        //{
        //    //Set up
        //    DisplayBorder border = new DisplayBorder('#', 1);
        //    DisplayBox box = new DisplayBox("MyBox", 0, 0, 80, 30, border);
        //    DisplayScreen screen = new DisplayScreen(150, 50);
        //    screen.Add(box);
        //    screen.WriteString("MyBox", "SomeText", ConsoleColor.Black, ConsoleColor.White, 0, 0);

        //    //Action
        //    screen.Clear();
        //    StringWriter sw = new StringWriter();
        //    Console.SetOut(sw);
        //    screen.PrintScreen();

        //    //Assertion

        //}
    }
}
