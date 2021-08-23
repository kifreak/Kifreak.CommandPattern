using System.Threading.Tasks;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;

namespace Kifreak.CommandPattern.Commands
{
    public class NotFoundCommand : ICommand
    {
        public string Name { get; set; }

        public Task Execute()
        {
            ConsoleHelper.Error($"Couldn't find command: {Name}");
            return Task.CompletedTask;
        }

        public bool Validate()
        {
            return true;
        }

    }
}