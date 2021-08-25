using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Configuration;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Helpers
{
    public static class CommandParser
    {
        internal static Dictionary<PropertyInfo, BaseAttribute> GetBaseAttributes(ICommand command)
        {
            return command
                .GetType()
                .GetProperties()
                .Select(property =>
                    new KeyValuePair<PropertyInfo, BaseAttribute>(property,
                        property.GetCustomAttributes()
                            .FirstOrDefault(attr => attr is BaseAttribute) 
                            as BaseAttribute
                    ))
                .Where(pair => pair.Value != null)
                .ToDictionary(pair => pair.Key, pair => pair.Value);


        }
        internal static ICommand ParseCommand(string[] args)
        {
            ICommandFactory command = FindRequestCommand(args[0]);
            if (command == null)
            {
                NotFoundCommand notFoundCommand = new NotFoundCommand {Name = args[0]};
                return notFoundCommand;
            }

            return command.MakeCommand(new Argument(args));
        }
        
        private static ICommandFactory FindRequestCommand(string commandName)
        {
            return Config.AvailableCommands.FirstOrDefault(t => string.Equals(t.CommandName, commandName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}