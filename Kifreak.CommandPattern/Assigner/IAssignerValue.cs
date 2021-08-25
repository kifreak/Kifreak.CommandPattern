using System.Reflection;

namespace Kifreak.CommandPattern.Assigner
{
    public interface IAssignerValue
    {
        string Name { get; }
        void SetValue(PropertyInfo propertyInfo, object target, string value);
    }
}