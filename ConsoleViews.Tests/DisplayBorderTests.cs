using ConsoleViews.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleViews.Tests
{
    public class DisplayBorderTests
    {
        [Theory]
        [InlineData(' ')]
        public void NewBorder(char symbol)
        {
            new DisplayBorder(symbol);
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(127, 0, 0, 0)]
        [InlineData(0, 127, 0, 0)]
        [InlineData(0, 0, 127, 0)]
        [InlineData(0, 0, 0, 127)]
        public void NewBorderWithValidThickness1(int borderThickness_left, int borderThickness_top, int borderThickness_right, int borderThickness_bottom)
        {
            DisplayBorder border = new DisplayBorder('#', borderThickness_left, borderThickness_top, borderThickness_right, borderThickness_bottom);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(127)]
        public void NewBorderWithValidThickness2(int borderThickness)
        {
            DisplayBorder border = new DisplayBorder('#', borderThickness);
        }

        [Theory]
        [InlineData(-1, 0, 0, 0)]
        [InlineData(0, -1, 0, 0)]
        [InlineData(0, 0, -1, 0)]
        [InlineData(0, 0, 0, -1)]
        [InlineData(128, 0, 0, 0)]
        [InlineData(0, 128, 0, 0)]
        [InlineData(0, 0, 128, 0)]
        [InlineData(0, 0, 0, 128)]
        public void NewBorderWithInvalidThickness(int borderThickness_left, int borderThickness_top, int borderThickness_right, int borderThickness_bottom)
        {
            Assert.Throws<ArgumentException>(() => new DisplayBorder('#', borderThickness_left, borderThickness_top, borderThickness_right, borderThickness_bottom));
        }
    }
}
