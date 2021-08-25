using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Attributes
{
    /// <summary>
    /// First attribute
    /// </summary>
    public class MainAttribute: BaseAttribute
    {
        public override string GetValue(Argument argument)
        {
            return argument.GetParameterByPosition(1);
        }

        public MainAttribute() : this(null)
        {

        }
        public MainAttribute(string description) : base(description)
        {
        }

    }
    
}