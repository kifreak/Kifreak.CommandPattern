using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Attributes
{
    public class SubAttribute : BaseAttribute
    {
        private int Position { get; }

        public SubAttribute(): this(0, null){}
        public SubAttribute(int position, string description): base(description)
        {
            Position = position;
        }

        public override string GetValue(Argument argument)
        {
            return argument.GetParameterByPosition(Position);
        }

        public override string GetName()
        {
            return $"Position number {Position}";
        }
    }
}