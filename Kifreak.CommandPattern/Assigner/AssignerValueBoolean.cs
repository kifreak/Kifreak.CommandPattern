using System;
using System.Reflection;

namespace Kifreak.CommandPattern.Assigner
{
    public class AssignerValueBoolean : IAssignerValue
    {
        public string Name => nameof(Boolean);
        public void SetValue(PropertyInfo propertyInfo, object target, string value)
        {
            if (propertyInfo != null && bool.TryParse(value, out bool valueBoolean))
            {
                propertyInfo.SetValue(target, valueBoolean);
            }
        }
    }
}