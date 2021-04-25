using ConsoleViews.Display;
using ConsoleViews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleViews.Tests
{
    public class DisplayBoxTests
    {
        [Theory]
        [InlineData("Box1", 0, 0, 0, 0)]
        [InlineData("Box1", 255, 0, 0, 0)]
        [InlineData("Box1", 0, 255, 0, 0)]
        [InlineData("Box1", 0, 0, 255, 0)]
        [InlineData("Box1", 0, 0, 0, 255)]
        [InlineData("ABCDABCDABCDABCDABCDABCDABCDABCDABCDABCD", 0, 0, 0, 0)]
        public void NewBoxWithValidArgumentsValues(string name, int x, int y, int width, int height)
        {
            DisplayBox box = new DisplayBox(name, x, y, width, height);
        }

        [Theory]
        [InlineData("Box1", -1, 0, 0, 0)]
        [InlineData("Box1", 0, -1, 0, 0)]
        [InlineData("Box1", 0, 0, -1, 0)]
        [InlineData("Box1", 0, 0, 0, -1)]
        [InlineData("Box1", 256, 0, 0, 0)]
        [InlineData("Box1", 0, 256, 0, 0)]
        [InlineData("Box1", 0, 0, 256, 0)]
        [InlineData("Box1", 0, 0, 0, 256)]
        [InlineData("", 0, 0, 0, 0)]
        [InlineData(null, 0, 0, 0, 0)]
        [InlineData("12345678901234567890123456789012345678901", 0, 0, 0, 0)]
        public void NewBoxWithInvalidArgumentsThrowsException(string name, int x, int y, int width, int height)
        {
            Assert.Throws<ArgumentException>(() => new DisplayBox(name, x, y, width, height));
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
        public void NewBoxWithInvalidBordersThrowException(int borderThickness_left, int borderThickness_top,
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
        public void NewBoxBordersAreValid(int borderThickness_left, int borderThickness_top,
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

    }
}
