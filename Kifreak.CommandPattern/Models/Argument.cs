using System;
using System.Collections.Generic;
using System.Linq;
using Kifreak.CommandPattern.Helpers;

namespace Kifreak.CommandPattern.Models
{
    public class Argument
    {
        public Argument(string[] arguments)
        {
            Arguments = arguments;
        }
        public string[] Arguments { get; set; }

        public string GetFirstParameter()
        {
            return GetParameters(0, 1).FirstOrDefault();
        }
        public string[] GetParameters(int minimum)
        {
            return GetParameters(1, minimum);
        }

        public string GetParameterByPosition(int position)
        {
            return Arguments.Length > position ? Arguments[position] : null;
        }
        public string[] GetParameters(int skip, int minimum)
        {
            if (Arguments == null || Arguments.Length == 0 || Arguments.Length < minimum)
            {
                return null;
            }

            string[] toLabels = new string[Arguments.Length - skip];
            var iteration = 0;
            for (var i = skip; i < Arguments.Length; i++)
            {
                toLabels[iteration] = Arguments[i];
                iteration++;
            }

            return toLabels;
        }

        public string GetOptionalParameter(string shortCommand, string longCommand, string defaultValue)
        {
            KeyValuePair<int, string> envString = GetCommandByName(shortCommand, longCommand);
            if (envString.Value == null)
            {
                return defaultValue;
            }
            if (Arguments.Length <= envString.Key + 1)
            {
                ConsoleHelper.Error($"After a -{shortCommand} or --{longCommand} you have to define a valid {longCommand}");
                if (!string.IsNullOrEmpty(defaultValue))
                {
                    ConsoleHelper.Error($"For {longCommand} selecting default value: {defaultValue}");
                }

                return string.Empty;
            }
            string argsValue = Arguments[envString.Key + 1];
            return !string.IsNullOrEmpty(argsValue) ? argsValue : string.Empty;
        }

        public bool HasOptionalParameter(string shortCommand, string longCommand)
        {
            KeyValuePair<int, string> envString = GetCommandByName(shortCommand, longCommand);
            return envString.Value != null;
        }

        private KeyValuePair<int, string> GetCommandByName(string shortCommand, string longCommand)
        {
            return Arguments.Select((value, key) => new KeyValuePair<int, string>(key, value)).FirstOrDefault(pair =>
                pair.Value == "-" + shortCommand || pair.Value.Equals("--" + longCommand, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}