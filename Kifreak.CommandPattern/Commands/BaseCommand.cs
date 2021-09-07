using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;
using Kifreak.CommandPattern.Output;

namespace Kifreak.CommandPattern.Commands
{
    public abstract class BaseCommand : ICommand, ICommandFactory
    {
        protected IOutput Output { get; }

        protected BaseCommand(IOutput output)
        {
            Output = output;
        }

        private Dictionary<string, string> _optionsDescription;
        public abstract Task Execute();
        public abstract bool Validate();

        public abstract string CommandName { get; }
        public abstract string Description { get; }

        public Dictionary<string, string> OptionsDescription
        {
            get
            {
                return _optionsDescription ??= CommandParser.GetBaseAttributes(this).Select(
                        pair => new
                            KeyValuePair<string, string>(pair.Key.Name, pair.Value.Description))
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

        public virtual ICommand MakeCommand(Argument argument)
        {
            AttributeFactory factory = new AttributeFactory();
            factory.Factory(this, argument);
            return this;
        }

    }
}