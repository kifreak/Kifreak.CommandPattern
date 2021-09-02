﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kifreak.CommandPattern.Models;

namespace Kifreak.CommandPattern.Attributes
{
    public class AttributeFactory
    {
        public void Factory(object parsedObject, Argument argument)
        {
            IEnumerable<KeyValuePair<PropertyInfo, BaseAttribute>> customProperties = parsedObject.GetType()
                .GetProperties().Select(property =>
                    new KeyValuePair<PropertyInfo, BaseAttribute>(property,
                        property.GetCustomAttributes().FirstOrDefault(t => t.GetType().BaseType is
                            {Name: nameof(BaseAttribute)}) as BaseAttribute))
                .Where(pair => pair.Value != null);
            foreach (KeyValuePair<PropertyInfo, BaseAttribute> keyValuePair in customProperties)
            {
                try
                {
                    keyValuePair.Value.SetAttribute(parsedObject, argument, keyValuePair.Key);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failing setting attribute to {keyValuePair.Key.Name}. Check if the attribute is public or has a setter");
                }
            }
        }
    }
}