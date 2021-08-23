using System.Collections.Generic;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Commands
{
    public abstract class BaseCommand : ICommand, ICommandFactory
    {
        public abstract Task Execute();
        public abstract bool Validate();
        
        public abstract string CommandName { get; }
        public abstract string Description { get; }
        public abstract Dictionary<string, string> OptionsDescription { get; }
        public abstract ICommand MakeCommand(Argument arguments);

    }
}