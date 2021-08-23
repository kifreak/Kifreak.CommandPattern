using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Kifreak.CommandPattern.Commands;
using Kifreak.CommandPattern.Configuration;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Helpers
{
    public static class CommandParser
    {
              
        public static string GetFirstParameter(string[] arguments)
        {
            return GetParameters(arguments, 1, 1).FirstOrDefault();
        }
        public static string[] GetParameters(string[] arguments, int minimum)
        {
            return GetParameters(arguments, 1, minimum);
        }
        public static string[] GetParameters(string[] arguments, int skip, int minimum)
        {
            if (arguments.Length < minimum)
            {
                return null;
            }
            string[] toLabels = new string[arguments.Length - skip];
            var iteration = 0;
            for (var i = skip; i < arguments.Length; i++)
            {
                toLabels[iteration] = arguments[i];
                iteration++;
            }

            return toLabels;
        }

        public static bool HasOptionalParameter(string shortCommand, string longCommand, string[] arguments)
        {
            KeyValuePair<int, string> envString = arguments.Select((value, key) => new KeyValuePair<int, string>(key, value)).FirstOrDefault(pair =>
                pair.Value == "-" + shortCommand || pair.Value.Equals("--" + longCommand, StringComparison.CurrentCultureIgnoreCase));
            return envString.Value != null;
        }
        public static string GetOptionalParameter(string shortCommand, string longCommand, string[] arguments, string defaultValue)
        {
            KeyValuePair<int, string> envString = arguments.Select((value, key) => new KeyValuePair<int, string>(key, value)).FirstOrDefault(pair =>
                pair.Value == "-" + shortCommand || pair.Value.Equals("--" + longCommand, StringComparison.CurrentCultureIgnoreCase));
            if (envString.Value == null)
            {
                return defaultValue;
            }
            if (arguments.Length <= envString.Key + 1)
            {
                ConsoleHelper.Error($"After a -{shortCommand} or --{longCommand} you have to define a valid {longCommand}");
                if (!string.IsNullOrEmpty(defaultValue))
                {
                    ConsoleHelper.Error($"For {longCommand} selecting default value: {defaultValue}");
                }

                return string.Empty;
            }
            string argsValue = arguments[envString.Key + 1];
            if (!string.IsNullOrEmpty(argsValue))
            {
                return argsValue;
            }

            return string.Empty;
        }
        internal static ICommand ParseCommand(string[] args)
        {
            ICommandFactory command = FindRequestCommand(args[0]);
            if (command == null)
            {
                NotFoundCommand notFoundCommand = new NotFoundCommand {Name = args[0]};
                return notFoundCommand;
            }

            return command.MakeCommand(new Argument {Arguments = args});
        }
        
        private static ICommandFactory FindRequestCommand(string commandName)
        {
            return Config.AvailableCommands.FirstOrDefault(t => string.Equals(t.CommandName, commandName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}