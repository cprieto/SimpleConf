using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using PropertyDescriptor = Castle.Components.DictionaryAdapter.PropertyDescriptor;

namespace SimpleConf
{
    public class KeyMustExistBehaviour : DictionaryBehaviorAttribute, IDictionaryPropertyGetter, IPropertyDescriptorInitializer
    {
        public object GetPropertyValue(IDictionaryAdapter dictionaryAdapter, string key, object storedValue,
            PropertyDescriptor property, bool ifExists)
        {
            var defaultValue = property.Annotations
                .OfType<DefaultValueAttribute>()
                .SingleOrDefault();

            if (storedValue == null && IsRequired(ifExists) && defaultValue == null)
                throw new KeyNotFoundException("key '" + key + "' not found");

            if (storedValue == null && defaultValue != null)
                return defaultValue.Value;

            return storedValue;
        }

        public void Initialize(PropertyDescriptor propertyDescriptor, object[] behaviors)
        {
            propertyDescriptor.Fetch = true;
        }

        private static bool IsRequired(bool ifExists)
        {
            return ifExists == false;
        }
    }
}