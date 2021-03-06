using System;
using System.IO;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Output;

namespace Kifreak.CommandPattern
{
    public static class CommandPattern
    {
        public static async Task Execute(string[] args, IOutput output)
        {
            await Execute(null, args, null, output);
        }
        public static async Task Execute(Action startProgram, string[] args, Action endProgram, IOutput output)
        {
            if (startProgram == null)
            {
                ConsoleHelper.WriteLineDarkBlue("Welcome to my program!!");
            }
            else
            {
                startProgram.Invoke();
            }

            if (args.Length == 0)
            {
                var helpCommand = new HelpCommand(output);
                await helpCommand.Execute();
                return;
            }

            ICommand command = CommandParser.ParseCommand(args, output);
            if (command.Validate())
            {
                await command.Execute();
            }

            SaveLog();
            if (endProgram == null)
            {
                ConsoleHelper.EndProgram();
            }
            else
            {
                endProgram.Invoke();
            }

        }

        private static void SaveLog()
        {
            StreamWriter writer = new StreamWriter($"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_output.html", false);
            writer.WriteLine("<html><head><title>LOGS</title></head><style>body {border-bottom-left-radius: 15px;border-bottom-right-radius: 15px;box-sizing: border-box;padding: 20px;background-color: #0C0C0C;color: #63de00;}</style><body>");
            ConsoleHelper.HtmlList.ForEach(writer.WriteLine);
            writer.WriteLine("</body></html>");
            writer.Close();
        }
    }
}