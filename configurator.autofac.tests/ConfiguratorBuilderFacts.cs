using System;
using Xunit;

namespace configurator.autofac.tests
{
    public interface ISimpleConfiguration
    {
        string Name { get; }
    }

    public class ConfiguratorBuilderFacts : IDisposable
    {
        public const string Key = "name";
        public readonly string Value = Guid.NewGuid().ToString("N");

        public ConfiguratorBuilderFacts()
        {
            Environment.SetEnvironmentVariable(Key, Value);
        }

        [Fact]
        public void ItCorrectlyGeneratesConfiguration()
        {
            var builder = new ConfiguratorBuilder<ISimpleConfiguration>();
            var configuration = builder
                .FromEnvironment().Build();

            Assert.NotNull(configuration);
            Assert.Equal(Value, configuration.Name);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(Key, null);
        }
    }
}