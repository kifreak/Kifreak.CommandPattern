using System;
using System.Collections.Generic;
using System.Linq;
using Kifreak.CommandPattern.Interfaces;

namespace Kifreak.CommandPattern.Configuration
{
    public static class Config
    {
        private static List<ICommandFactory> _availableCommands;

        public static List<ICommandFactory> AvailableCommands => _availableCommands ?? GetAvailableCommands();

        internal static List<ICommandFactory> GetAvailableCommands()
        {
            if (_availableCommands== null ||  _availableCommands.Count == 0)
            {
                var iCommandFactory = typeof(ICommandFactory);
                _availableCommands = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => iCommandFactory.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    .Select(type => Activator.CreateInstance(type) as ICommandFactory).ToList();
            }

            return _availableCommands;
        }

        public static ICommandFactory Get(string name)
        {
            return AvailableCommands.FirstOrDefault(t => t.CommandName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public static T Get<T>() where T : ICommand
        {
            ICommandFactory command = AvailableCommands.FirstOrDefault(t => t.GetType().Name == typeof(T).Name);
            return (T) command;
        }
    }

}