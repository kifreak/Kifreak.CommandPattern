using System;
using System.Collections.Generic;
using System.Linq;
using Kifreak.CommandPattern.Assigner;
using Kifreak.CommandPattern.Attributes;
using Kifreak.CommandPattern.Interfaces;

namespace Kifreak.CommandPattern.Configuration
{
    public static class Config
    {
        private static List<ICommandFactory> _availableCommands;
        private static List<BaseAttribute> _availableAttributes;
        private static List<IAssignerValue> _assignerValues;
        public static List<ICommandFactory> AvailableCommands => _availableCommands ?? GetAvailableCommands();

        public static List<BaseAttribute> AvailableAttributes = _availableAttributes ?? GetAvailableAttributes();

        public static List<IAssignerValue> AssignerValues => _assignerValues ?? GetAssignerValues();
        internal static List<BaseAttribute> GetAvailableAttributes()
        {
            if (_availableAttributes == null || _availableAttributes.Count == 0)
            {
                var baseAttributeType = typeof(BaseAttribute);
                _availableAttributes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => baseAttributeType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    .Select(type => Activator.CreateInstance(type) as BaseAttribute).ToList();
            }

            return _availableAttributes;
        }

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