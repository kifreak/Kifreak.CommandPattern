using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Configuration;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;
using Kifreak.CommandPattern.Output;

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
            Output.EmptyLine(1);
            Output.Color("This is a helper page.", "White");
            Output.Color("If you need more information about an action. Write:", "White");
            Output.Color("help actionName", "DarkGreen");
            Output.EmptyLine(1);
            IEnumerable<ICommandFactory> availableCommands = Config.GetAvailableCommands(Output);
            foreach (var availableCommand in availableCommands)
            {
                Output.Separator();
                Output.Info($"{availableCommand.CommandName}: ");
                Output.Color(availableCommand.Description,"White");
            }
            ConsoleHelper.WriteLineSeparator();
        }

        public void ShowSpecificHelp()
        {
            IEnumerable<ICommandFactory> availableCommands = Config.GetAvailableCommands(Output);
            ICommandFactory command = availableCommands.FirstOrDefault(
                t => t.CommandName.Equals(Topic, StringComparison.CurrentCultureIgnoreCase));
            if (command == null)
            {
                Output.Error("The introduced command doesn't exist.");
                return;
            }
            Output.Separator();
            Output.Info($"{command.CommandName}: ");
            Output.Color(command.Description, "White");
            Output.Color("Arguments:", "DarkBlue");
            Dictionary<PropertyInfo, BaseAttribute> attributes = CommandParser.GetBaseAttributes(command as ICommand);
            foreach (KeyValuePair<PropertyInfo, BaseAttribute> pair in attributes)
            {
                Output.Color($"{pair.Key.Name} ({pair.Value.GetName()}): {pair.Value.Description}", "White");
            }
            
        }

        public override bool Validate()
        {
            return true;
        }

        #endregion ICommand

        #region ICommandFactory

        public override string CommandName => "Help";
        public override string Description => "Show this help page.";

        public override ICommand MakeCommand(Argument argument)
        {
            var topic = string.Empty;
            if (argument.Arguments != null && argument.Arguments.Length > 1)
            {
                topic = argument.Arguments[1];
            }

            return new HelpCommand(Output) {Topic = topic};
        }

        #endregion ICommandFactory

        public HelpCommand(IOutput output) : base(output)
        {
        }
    }
}