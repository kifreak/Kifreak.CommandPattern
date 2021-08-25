using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Helpers;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Commands
{
    public abstract class BaseCommand : ICommand, ICommandFactory
    {

        private Dictionary<string, string> _optionsDescription;
        public abstract Task Execute();
        public abstract bool Validate();

        public abstract string CommandName { get; }
        public abstract string Description { get; }

        public Dictionary<string, string> OptionsDescription
        {
            get
            {
                if (_optionsDescription == null)
                {
                    _optionsDescription = CommandParser.GetBaseAttributes(this).Select(pair => new
                            KeyValuePair<string, string>(pair.Key.Name, pair.Value.Description))
                        .ToDictionary(pair => pair.Key, pair => pair.Value);
                }

                return _optionsDescription;
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