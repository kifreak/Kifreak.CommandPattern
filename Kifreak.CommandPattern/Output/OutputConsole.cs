using System;
using Kifreak.CommandPattern.Helpers;

namespace Kifreak.CommandPattern.Output
{
    public class OutputConsole : IOutput
    {
        public void Info(string message)
        {
            ConsoleHelper.Info(message);
        }

        public void Color(string message, string color)
        {
            if (Enum.TryParse(color, out ConsoleColor consoleColor))
            {
                ConsoleHelper.WriteLineInColor(message, consoleColor);
            }
            else
            {
                ConsoleHelper.Info(message);
            }
            
        }

        public void Warning(string message)
        {
            ConsoleHelper.WriteLineDarkBlue(message);
        }

        public void Error(string message)
        {
            ConsoleHelper.Error(message);
        }

        public void EmptyLine(int numberOfLines)
        {
            ConsoleHelper.JumpLine(numberOfLines);
        }

        public void Separator()
        {
            ConsoleHelper.WriteLineSeparator();
        }
    }
}