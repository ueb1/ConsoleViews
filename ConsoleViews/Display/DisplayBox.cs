using ConsoleViews.Cells;
using ConsoleViews.Display.Entities;
using ConsoleViews.Enums;
using ConsoleViews.Exceptions;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleViews.Display
{
    public class DisplayBox
    {
        public string Name { get; private set; }
        public short DisplayWidth { get; private set; }
        public short DisplayHeight { get; private set; }
        public short DisplayX { get; private set; }
        public short DisplayY { get; private set; }
        public DisplayBorder Border { get; private set; }
        public Rect Rectangle { get; private set; }

        public DisplayBox(string name, int x, int y, int width, int height, DisplayBorder border = null)
        {
            if(border != null)
            {
                int halfWidth = width / 2;
                int halfHeight = height / 2;
                if(Math.Max(
                        border.Thickness[DisplayBorder.LEFT], 
                        border.Thickness[DisplayBorder.RIGHT]) > halfWidth ||
                    Math.Max(
                        border.Thickness[DisplayBorder.TOP], 
                        border.Thickness[DisplayBorder.BOTTOM]) > halfHeight)
                {
                    throw new InvalidBorderException("Border thickness cannot be greater than 50% of box width/height");
                }
                else if(new int[] { 
                    border.Thickness[DisplayBorder.LEFT], 
                    border.Thickness[DisplayBorder.TOP], 
                    border.Thickness[DisplayBorder.RIGHT], 
                    border.Thickness[DisplayBorder.BOTTOM] }.Min() < 0)
                {
                    throw new InvalidBorderException("Border thickness cannot be less than zero");
                }
            }
            Name = name;
            DisplayWidth = (short)width;
            DisplayHeight = (short)height;

            DisplayX = (short)x;
            DisplayY = (short)y;
            Border = border;

            Rectangle = new Rect(x, y, width, height);
        }
    }
}
