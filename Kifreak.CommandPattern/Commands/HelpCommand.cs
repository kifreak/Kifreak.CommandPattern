using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Configuration;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Commands
{
    public class HelpCommand : BaseCommand
    {
        #region ICommand

        [Main(description: "Write the command you need help with.")]
        public string Topic { get; set; }

        public override Task Execute()
        {
            if (string.IsNullOrEmpty(Topic))
            {
                ShowBasicHelp();
            }
            else
            {
                ShowSpecificHelp();
            }

            return Task.CompletedTask;
        }

        public void ShowBasicHelp()
        {
            ConsoleHelper.JumpLine(1);
            System.Console.WriteLine("This is a helper page.");
            System.Console.WriteLine("If you need more information about an action. Write:");
            ConsoleHelper.WriteLineDarkGreen("help actionName");
            ConsoleHelper.JumpLine(1);
            IEnumerable<ICommandFactory> availableCommands = Config.GetAvailableCommands();
            foreach (var availableCommand in availableCommands)
            {
                ConsoleHelper.WriteLineSeparator();
                ConsoleHelper.WriteDarkYellow($"{availableCommand.CommandName}: ");
                Console.WriteLine(availableCommand.Description);
            }
            ConsoleHelper.WriteLineSeparator();
        }

        public void ShowSpecificHelp()
        {
            IEnumerable<ICommandFactory> availableCommands = Config.GetAvailableCommands();
            ICommandFactory command = availableCommands.FirstOrDefault(
                t => t.CommandName.Equals(Topic, StringComparison.CurrentCultureIgnoreCase));
            if (command == null)
            {
                ConsoleHelper.Error("The introduced command doesn't exist.");
                return;
            }
            ConsoleHelper.WriteLineSeparator();
            ConsoleHelper.WriteDarkYellow($"{command.CommandName}: ");
            Console.WriteLine(command.Description);
            ConsoleHelper.WriteLineDarkBlue("Arguments:");
            throw new NotImplementedException();
            
        }

        public override bool Validate()
        {
            return true;
        }

        #endregion ICommand

        #region ICommandFactory

        public override string CommandName => "Help";
        public override string Description => "Show this help page.";

        //public override Dictionary<string, string> OptionsDescription => new Dictionary<string, string>
        //    { {"Command", "Write the command you need help with."}};

        public override ICommand MakeCommand(Argument argument)
        {
            var topic = string.Empty;
            if (argument.Arguments != null && argument.Arguments.Length > 1)
            {
                topic = argument.Arguments[1];
            }

            return new HelpCommand {Topic = topic};
        }

        #endregion ICommandFactory
    }
}