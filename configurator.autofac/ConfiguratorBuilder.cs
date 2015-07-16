using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace configurator.autofac
{
    public sealed class ConfiguratorBuilder<T> where T : class
    {
        private readonly IList<IDictionary<string, string>> _sources = new List<IDictionary<string, string>>();

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
            _sources.Add(source.GetValues());

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

            _sources.Add(source.GetValues());

            return this;
        }

        private string _prefix = String.Empty;
        private string _separator = ":";

        public T Build()
        {
            var factory = new DictionaryAdapterFactory();
            var meta = factory.GetAdapterMeta(typeof (T));
            var propertyDescriptor = new PropertyDescriptor();

            var adapter = new CascadingMultipleDictionaryAdapter(_sources);
            var config = meta.CreateInstance(adapter, propertyDescriptor) as T;
            return config;
        }
    }
}