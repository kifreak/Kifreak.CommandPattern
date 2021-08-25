using System.Threading.Tasks;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Helpers;

namespace Kifreak.CommandPattern.Console
{
    public class TestCommand : BaseCommand
    {
        [Main("You're name")] 
        public string Name { get; set; }
        [Optional("t", "tall", "Your tall")]
        public string Tall { get; set; }

        [OptionalNoValue("m", "male", "Select if you are a male")]
        public bool IsMale { get; set; }
        [OptionalNoValue("f","female", "Select if your are a female")]
        public bool IsFemale { get; set; }

        public EGender Gender {
            get
            {
                if (IsMale && IsFemale)
                {
                    return EGender.NotDefined;
                }
                else if (IsMale)
                {
                    return EGender.Male;
                }
                else
                {
                    return EGender.Female;
                }
            }
        }
        public override Task Execute()
        {
            ConsoleHelper.WriteLineDarkYellow($"Hello World!! Thanks to you, {Name}.");
            if (!string.IsNullOrEmpty(Tall))
            {
                ConsoleHelper.WriteLineDarkYellow($"You're so {Tall} tall");
            }

            if (Gender == EGender.NotDefined)
            {
                ConsoleHelper.WriteLineDarkYellow("You're decided not to share you're gender");
            }
            else
            {
                ConsoleHelper.WriteLineDarkYellow($"You're {Gender}");
            }
            return Task.CompletedTask;
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(Name);
        }


        public override string CommandName => "HelloWorld";
        public override string Description => "Say hello to the world";
        
    }
}