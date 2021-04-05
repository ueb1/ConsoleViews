using BestiLeikurinn;
using BestiLeikurinn.Display.Entities;
using BestiLeikurinn.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

public class ConsoleHelper
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int cursorX { get; private set; }
    public int cursorY { get; private set; }
    public List<List<ConsoleString>> Screen { get; private set; }
    public ConsoleColor DefaultTextColor { get; private set; }
    public ConsoleColor DefaultBackgroundColor { get; private set; }


    public ConsoleHelper(int width, int height, 
        ConsoleColor defaultTextColor = ConsoleColor.White, 
        ConsoleColor defaultBackgroundColor = ConsoleColor.Black)
    {
        Width = width;
        Height = height;
        cursorX = 0;
        cursorY = 0;
        Screen = new List<List<ConsoleString>>();
        for(int i=0; i<Height; i++)
        {
            Screen.Add(new List<ConsoleString>());
        }
        DefaultBackgroundColor = defaultBackgroundColor;
        DefaultTextColor = defaultTextColor;
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
                if(originalString.Length > split + newString.Length)
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
    public void PrintScreen()
    {
        foreach(List<ConsoleString> stringList in Screen)
        {
            foreach(ConsoleString consoleString in stringList)
            {
                Console.BackgroundColor = consoleString.BackgroundColor;
                Console.ForegroundColor = consoleString.TextColor;
                Console.Write(consoleString.Text);
            }
            Console.WriteLine();
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