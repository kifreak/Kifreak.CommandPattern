using System;
using System.Collections.Generic;

namespace Kifreak.CommandPattern.Helpers
{
    public static class ConsoleHelper
    {
        public static List<string> HtmlList = new List<string>();

        public static void Error(string message)
        {
            WriteLineInColor($"Error: {message}", ConsoleColor.Red);
        }

        public static void Info(string message)
        {
            WriteLineDarkYellow(message);
        }

        public static void EndProgram()
        {
            WriteLineDarkGreen("Program finish. Press any key to close this window.");
            Console.ReadKey();
        }

        public static void WriteLineDarkYellow(string message)
        {
            WriteLineInColor(message, ConsoleColor.DarkYellow);
        }

        public static void WriteDarkYellow(string message)
        {
            WriteInColor(message, ConsoleColor.DarkYellow);
        }

        public static void WriteDarkRed(string message)
        {
            WriteInColor(message, ConsoleColor.DarkRed);
        }

        public static void WriteLineDarkRed(string message)
        {
            WriteLineInColor(message, ConsoleColor.DarkRed);
        }

        public static void WriteLineDarkGreen(string message)
        {
            WriteLineInColor(message, ConsoleColor.DarkGreen);
        }

        public static void WriteLineDarkBlue(string message)
        {
            WriteLineInColor(message, ConsoleColor.DarkBlue);
        }

        public static void WriteLineSeparator()
        {
            string message =
                "======================================================================================================================================";
            LineToHtml(message, ConsoleColor.White);
            Console.WriteLine(message);
        }

        public static void JumpLine(int numberOfLinesToJump)
        {
            for (var i = 0; i < numberOfLinesToJump; i++)
            {
                EmptyLineHtml();
                Console.WriteLine();
            }
        }

        public static void WriteLineInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            LineToHtml(message,color);
        }

        public static void WriteInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            ToHtml(message, color);
        }

        private static void LineToHtml(string message, ConsoleColor color)
        {
            HtmlList.Add($"<p style=\"color:{ConvertColorToHtml(color)};line-height:20px;margin:0px;\">{message}</p>");
        }

        private static void ToHtml(string message, ConsoleColor color)
        {
            HtmlList.Add($"<span style=\"color:{ConvertColorToHtml(color)}\">{message}</span>");
        }

        private static string ConvertColorToHtml(ConsoleColor color)
        {
            Dictionary<ConsoleColor, string> colors = new Dictionary<ConsoleColor, string>
            {
                {ConsoleColor.DarkBlue, "0037DA"},
                {ConsoleColor.DarkYellow, "C19C00"},
                {ConsoleColor.Red, "E7484B"},
                {ConsoleColor.DarkRed, "C50F1F"},
                {ConsoleColor.DarkGreen, "13A10E"},
                {ConsoleColor.White, "F2F2F2"}
            };
            if (!colors.ContainsKey(color))
            {
                Info($"Color: {color} not found in dictionary");
            }
            return colors.ContainsKey(color) ? colors[color] : color.ToString().ToLower();
        }

        private static void EmptyLineHtml()
        {
            HtmlList.Add("<p></p>");
        }
        
    }
}