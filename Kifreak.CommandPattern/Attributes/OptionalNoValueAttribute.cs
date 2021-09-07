using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Attributes
{
    public class OptionalNoValueAttribute : BaseAttribute
    {
        private readonly string _shortCommand;
        private readonly string _longCommand;
        
        public OptionalNoValueAttribute(string shortCommand, string longCommand, string description): base(description)
        {
            _shortCommand = shortCommand;
            _longCommand = longCommand;
        }
        
        public OptionalNoValueAttribute(string longCommand, string description): this(null, longCommand, description)
        {
        }

        public OptionalNoValueAttribute(): this(null,null){}

        public override string GetValue(Argument argument)
        {
            return argument.HasOptionalParameter(_shortCommand, _longCommand).ToString();
        }

        public override string GetName()
        {

            string shortCommandDescription = !string.IsNullOrEmpty(_shortCommand) ? $" (-{_shortCommand})" : string.Empty;
            return $"{_longCommand}{shortCommandDescription}";
        }
    }
}