using System;
using System.Collections.Generic;
using System.Linq;
using Kifreak.CommandPattern.Assigner;
using Kifreak.CommandPattern.Interfaces;
using Kifreak.CommandPattern.Output;

namespace Kifreak.CommandPattern.Configuration
{
    public static class Config
    {
        private static List<ICommandFactory> _availableCommands;

        private static List<IAssignerValue> _assignerValues;
        
        public static List<IAssignerValue> AssignerValues => _assignerValues ?? GetAssignerValues();
        
        public static List<ICommandFactory> GetAvailableCommands(IOutput output)
        {
            if (_availableCommands== null ||  _availableCommands.Count == 0)
            {
                var iCommandFactory = typeof(ICommandFactory);
                Object[] args = { output };
                _availableCommands = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => iCommandFactory.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    .Select(type => Activator.CreateInstance(type, args) as ICommandFactory).ToList();
            }

            return _availableCommands;
        }

        internal static List<IAssignerValue> GetAssignerValues()
        {
            if (_assignerValues == null || _assignerValues.Count == 0)
            {
                var iCommandFactory = typeof(IAssignerValue);
                _assignerValues = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => iCommandFactory.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    .Select(type => Activator.CreateInstance(type) as IAssignerValue).ToList();
            }

            return _assignerValues;
        }

    }

}