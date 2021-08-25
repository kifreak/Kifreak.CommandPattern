using System;
using System.Reflection;

namespace Kifreak.CommandPattern.Assigner
{
    public class AssignerValueInteger : IAssignerValue
    {
        public string Name => nameof(Int32);
        public void SetValue(PropertyInfo propertyInfo, object target, string value)
        {
            if (propertyInfo != null && int.TryParse(value, out int valueInteger))
            {

                propertyInfo.SetValue(target, valueInteger);
            }
        }
    }
}