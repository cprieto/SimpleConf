using System;
using Castle.Components.DictionaryAdapter;

namespace SimpleConf
{
    public class KeyMustExistBehaviour : DictionaryBehaviorAttribute, IDictionaryPropertyGetter, IPropertyDescriptorInitializer
    {
        public object GetPropertyValue(IDictionaryAdapter dictionaryAdapter, string key, object storedValue,
            PropertyDescriptor property, bool ifExists)
        {
            if (storedValue == null && IsRequired(ifExists))
                throw new Exception();
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