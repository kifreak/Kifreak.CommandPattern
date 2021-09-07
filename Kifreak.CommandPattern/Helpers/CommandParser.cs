using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Configuration;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;
using Kifreak.CommandPattern.Output;

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
        internal static ICommand ParseCommand(string[] args, IOutput output)
        {
            Argument argument = new Argument(args);
            string firstParameter = argument.GetFirstParameter();
            ICommandFactory command = FindRequestCommand(firstParameter, output);
            if (command == null)
            {
                return new NotFoundCommand(output) {Name = firstParameter};
            }

            return command.MakeCommand(argument);
        }
        
        private static ICommandFactory FindRequestCommand(string commandName, IOutput output)
        {
            return Config.GetAvailableCommands(output).FirstOrDefault(t => string.Equals(t.CommandName, commandName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}