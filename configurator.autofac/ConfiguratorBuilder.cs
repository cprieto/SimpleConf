using System;
using System.Collections.Generic;

namespace configurator.autofac
{
    public sealed class ConfiguratorBuilder
    {
        private readonly IList<IDictionary<string, string>> _sources = new List<IDictionary<string, string>>();

        public ConfiguratorBuilder FromEnvironment()
        {
            return FromSource(new EnvironmentConfigurationSource());
        }

        public ConfiguratorBuilder FromAppSettings()
        {
            return FromSource(new AppSettingsConfigurationSource());
        }

        public ConfiguratorBuilder FromSource<TSource>() where TSource : IConfigurationSource, new()
        {
            var source = Activator.CreateInstance<TSource>();
            _sources.Add(source.GetValues());

            return this;
        }

        public ConfiguratorBuilder FromSource<TSource>(TSource source) where TSource : IConfigurationSource
        {
            if (source == null)
                throw new ArgumentNullException();

            _sources.Add(source.GetValues());

            return this;
        }

        public IList<IDictionary<string, string>> Build()
        {
            return _sources;
        }
    }
}