using System;
using System.Linq;
using System.Reflection;
using Kifreak.CommandPattern.Assigner;
using Kifreak.CommandPattern.Configuration;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Attributes
{
    public abstract class BaseAttribute : Attribute
    {
        public string Description { get; }

        protected  BaseAttribute(): this(null) {}
        protected BaseAttribute(string description)
        {
            Description = description;
        }
        
        public void SetAttribute(object target, Argument argument, PropertyInfo propertyInfo)
        {
            IAssignerValue assignerValue = Config.AssignerValues.FirstOrDefault(assigner => assigner.Name == propertyInfo.PropertyType.Name);
            if (assignerValue == null)
            {
                throw new Exception($"Need to create an IAssignerValue for property {propertyInfo.Name}");
            }
            assignerValue.SetValue(propertyInfo, target, GetValue(argument));
         
        }

        public abstract string GetValue(Argument argument);
    }
}