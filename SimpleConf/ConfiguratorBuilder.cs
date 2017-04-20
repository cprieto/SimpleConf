using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;

namespace SimpleConf
{
    public sealed class ConfiguratorBuilder<T> where T : class
    {
        private readonly IList<IConfigurationSource> _sources = new List<IConfigurationSource>(); 

        public ConfiguratorBuilder<T> FromEnvironment()
        {
            return FromSource(new EnvironmentConfigurationSource());
        }

        public ConfiguratorBuilder<T> FromAppSettings()
        {
            return FromSource(new AppSettingsConfigurationSource());
        }

        public ConfiguratorBuilder<T> FromSource<TSource>() where TSource : IConfigurationSource, new()
        {
            var source = Activator.CreateInstance<TSource>();
            _sources.Add(source);

            return this;
        }

        public ConfiguratorBuilder<T> WithKeyPrefix(string prefix, string separator = ":")
        {
            _prefix = prefix;
            _separator = separator;
            return this;
        }

        public ConfiguratorBuilder<T> FromSource<TSource>(TSource source) where TSource : IConfigurationSource
        {
            if (source == null)
                throw new ArgumentNullException();

            _sources.Add(source);

            return this;
        }

        private string _prefix = string.Empty;
        private string _separator = ":";

        public T Build()
        {
            var factory = new DictionaryAdapterFactory();
            var meta = factory.GetAdapterMeta(typeof (T));
            var propertyDescriptor = new PropertyDescriptor();
            AddKeyPrefix(propertyDescriptor);
            propertyDescriptor.AddBehavior(new KeyMustExistBehaviour());

            foreach (var prop in meta.Properties)
            {
                prop.Value.Fetch = true;
            }

            var data = _sources.Select(x => x.GetValues());

            var adapter = new CascadingMultipleDictionaryAdapter(data);
            var config = meta.CreateInstance(adapter, propertyDescriptor) as T;
            return config;
        }

        private void AddKeyPrefix(PropertyDescriptor descriptor)
        {
            var prefix = string.IsNullOrWhiteSpace(_prefix) ? string.Empty 
                : $"{_prefix}{_separator}";

            var behavior = new KeyPrefixAttribute(prefix);
            
            descriptor.AddBehavior(behavior);
        }
    }
}