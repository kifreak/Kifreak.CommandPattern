using System;
using System.Reflection;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Output;

namespace Kifreak.CommandPattern.UnitTests
{

    public class BaseTestCommand: BaseCommand
    {
     
        [Main(description: "This is the type command")]
        public string GetTypeCommand { get; set; }

        [Sub(2, description: "This is a second type command")]
        public string GetSecondTypeCommand { get; set; }

        [OptionalNoValue("no-devices", "Define it's a non-devices option")]
        public bool NoDevices { get; set; }

        [OptionalNoValue("no-exist", "Define a no-exist option")]
        public bool NoExist { get; set; }

        [Optional("t","toText", "This is a default text", "Define the text to something")]
        public string ToText { get; set; }

        [Optional("e", "example", "This is an example", "This is just an example of a default value option")]
        public string Example { get; set; }
        public override Task Execute()
        {
            throw new NotImplementedException();
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }

        public override string CommandName => "TestCommand";
        public override string Description => "This is a test command";

        public BaseTestCommand(IOutput output) : base(output)
        {
        }
    }
}