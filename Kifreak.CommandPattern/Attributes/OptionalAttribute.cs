using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Attributes
{
    public class OptionalAttribute : BaseAttribute
    {
        private readonly string _shortCommand;
        private readonly string _longCommand;
        private readonly string _defaultValue;
        public OptionalAttribute(): this(null,null) {}
        public OptionalAttribute(string shortCommand, string longCommand, string defaultValue, string description): base(description)
        {
            _shortCommand = shortCommand;
            _longCommand = longCommand;
            _defaultValue = defaultValue;
        }
        public OptionalAttribute(string longCommand, string defaultValue, string description): this(null, longCommand, defaultValue, description)
        {
        }
        public OptionalAttribute(string longCommand, string description): this(longCommand,null, description)
        {
        }

        public override string GetValue(Argument argument)
        {
            return argument.GetOptionalParameter(_shortCommand, _longCommand, _defaultValue);
        }
    }
}