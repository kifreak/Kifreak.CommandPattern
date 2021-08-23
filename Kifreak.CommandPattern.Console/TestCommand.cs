using System.Collections.Generic;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Console
{
    public enum EGender{
        Female, Male, NotDefined
    }
    public class TestCommand: BaseCommand
    {
        private readonly string _name;
        private readonly string _tall;
        private readonly EGender _gender;

        public TestCommand()
        {

        }

        public TestCommand(string name, string tall, EGender gender)
        {
            _name = name;
            _tall = tall;
            _gender = gender;
        }
        public override Task Execute()
        {
            ConsoleHelper.WriteLineDarkYellow($"Hello World!! Thanks to you, {_name}.");
            if (!string.IsNullOrEmpty(_tall))
            {
                ConsoleHelper.WriteLineDarkYellow($"You're so {_tall} tall");
            }

            if (_gender == EGender.NotDefined)
            {
                ConsoleHelper.WriteLineDarkYellow("You're decided not to share you're gender");
            }
            else
            {
                ConsoleHelper.WriteLineDarkYellow($"You're {_gender}");
            }
            return Task.CompletedTask;
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(_name);
        }


        public override string CommandName => "HelloWorld";
        public override string Description => "Say hello to the world";

        public override Dictionary<string, string> OptionsDescription => new Dictionary<string, string>
        {
            {"name", "Your name"},
            { "-t/--tall", "Your tall"},
            {"-m/--male", "You're a male"},
            {"-f/--female", "You're a female"}
        };
        //TODO: DEFINED A ARGUMENTS CLASS WHERE WE ADDD COMMANDPARSER FUNCTIONS
        public override ICommand MakeCommand(Argument argument)
        {
            bool isMale = CommandParser.HasOptionalParameter("m", "male", argument.Arguments);
            bool isFemale = CommandParser.HasOptionalParameter("f", "female", argument.Arguments);
            EGender gender = EGender.Female;
            if ((isMale && isFemale) || (!isMale && !isFemale))
            {
                gender = EGender.NotDefined;
            } else if (isMale)
            {
                gender = EGender.Male;
            }
            //CommandParser.GetFirstParameter(arguments) same CommandParser.GetParameters(arguments, 1)
            return new TestCommand(CommandParser.GetFirstParameter(argument.Arguments), CommandParser.GetOptionalParameter("t","tall", argument.Arguments, null), gender);
        }
    }
}