using System;

namespace Kifreak.CommandPattern.Exceptions
{
    public class AttributeNotFoundException: Exception
    {
        public AttributeNotFoundException(string attributeName): base($"Failing setting attribute to {attributeName}. Check if the attribute is public or has a setter")
        {
            
        }
    }
}