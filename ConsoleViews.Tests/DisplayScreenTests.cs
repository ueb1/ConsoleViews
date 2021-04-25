using ConsoleViews.Display;
using ConsoleViews.Enums;
using ConsoleViews.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ConsoleViews.Tests
{
    public class DisplayScreenTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(150, 50)]
        public void DisplayScreen_NewScreenWithArgumentsValues(int width, int height)
        {
            DisplayScreen screen = new DisplayScreen(width, height);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(151, 0)]
        [InlineData(0, 51)]
        public void DisplayScreen_NewScreenWithInvalidArgumentsThrowsException(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => new DisplayScreen(width, height));
        }

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

        [Theory]
        [InlineData("Box1", 0, 0, 0, 0, 50, 50)]
        [InlineData("Box1", 0, 0, 50, 50, 50, 50)]
        public void Add_NewBoxGetsAddedCorrectly(string name, int x, int y, int width, int height, int screenWidth, int screenHeight)
        {
            //Set up
            DisplayScreen screen = new DisplayScreen(screenWidth, screenHeight);
            DisplayBox box = new DisplayBox(name, x, y, width, height);

            //Action
            screen.Add(box);
            List<DisplayBox> boxList = screen.GetDisplayBoxes();

            //Assert
            Assert.True(boxList.Count > 0);
            Assert.True(boxList[0].Name == name);
            Assert.True(boxList[0].DisplayX == x);
            Assert.True(boxList[0].DisplayY == y);
            Assert.True(boxList[0].DisplayWidth == width);
            Assert.True(boxList[0].DisplayHeight == height);
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
            screen.Add(box2);

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

        [Fact]
        public void Clear_OutputGetsCleared()
        {
            //Set up
            int lines = 10;
            int cols = 10;
            DisplayBorder border = new DisplayBorder('#', 1);
            DisplayBox box = new DisplayBox("MyBox", 0, 0, cols, lines, border);
            DisplayScreen screen = new DisplayScreen(cols, lines);
            screen.Add(box);
            screen.LoadBorders();
            screen.WriteString("MyBox", "SomeText", ConsoleColor.Black, ConsoleColor.White, 0, 0);
            string expectedString = string.Concat(Enumerable.Repeat(new string(' ', 10) + Environment.NewLine, 10));

            //Action
            screen.Clear();
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            screen.PrintScreen();
            string actualString = sw.ToString();

            //Assertion
            Assert.True(expectedString == actualString);
        }

        [Fact]
        public void Clear_DisplayBoxGetsCleared()
        {
            //Set up
            int boxLines = 10;
            int boxCols = 10;
            DisplayBorder border1 = new DisplayBorder('#', 1);
            DisplayBorder border2 = new DisplayBorder('#', 1);
            DisplayBox box1 = new DisplayBox("Box1", 0, 0, boxCols, boxLines, border1);
            DisplayBox box2 = new DisplayBox("Box2", 0, boxLines, boxCols, boxLines, border2);
            DisplayScreen screen = new DisplayScreen(10, 20);
            screen.Add(box1);
            screen.Add(box2);
            screen.LoadBorders();
            screen.WriteString("Box1", "SomeText", ConsoleColor.Black, ConsoleColor.White, 0, 0);
            screen.WriteString("Box2", "SomeText", ConsoleColor.Black, ConsoleColor.White, 0, 0);
            string expectedString1 = string.Concat(Enumerable.Repeat(new string(' ', 10) + Environment.NewLine, 10));

            //Action
            screen.Clear("Box1");
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            screen.PrintScreen();
            int splitPtr = (boxCols + 2) * boxLines;
            string actualString1 = sw.ToString().Substring(0, splitPtr);
            string actualString2 = sw.ToString().Substring(splitPtr);

            //Assertion
            Assert.Equal(expectedString1, actualString1);       // Box1 should be cleared
            Assert.NotEqual(expectedString1, actualString2);    // but not Box2
        }
    }
}

