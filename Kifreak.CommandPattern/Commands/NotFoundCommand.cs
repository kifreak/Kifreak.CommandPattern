using System.Threading.Tasks;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Output;

namespace Kifreak.CommandPattern.Commands
{
    public class NotFoundCommand : ICommand
    {
        private readonly IOutput _output;

        public NotFoundCommand(IOutput output)
        {
            _output = output;
        }
        public string Name { get; set; }

        public Task Execute()
        {
            _output.Error($"Couldn't find command: {Name}");
            return Task.CompletedTask;
        }

        public bool Validate()
        {
            return true;
        }

    }
}