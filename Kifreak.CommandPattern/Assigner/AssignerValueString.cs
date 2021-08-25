using System;
using System.Reflection;

namespace Kifreak.CommandPattern.Assigner
{
    public class AssignerValueString : IAssignerValue
    {
        public string Name => nameof(String);
        public void SetValue(PropertyInfo propertyInfo, object target, string value)
        {
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(target, value);
            }
        }
    }
}