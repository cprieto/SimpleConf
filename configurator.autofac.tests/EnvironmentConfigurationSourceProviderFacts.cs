using System;
using System.Collections.Generic;
using Xunit;

namespace configurator.autofac.tests
{
    public class EnvironmentConfigurationSourceProviderFacts : IDisposable
    {
        private readonly string _value;
        private readonly string _key;
        private readonly EnvironmentConfigurationSource _provider = new EnvironmentConfigurationSource();

        public EnvironmentConfigurationSourceProviderFacts()
        {
            _key = Guid.NewGuid().ToString("N");
            _value = new Random().Next(10).ToString();

            Environment.SetEnvironmentVariable(_key, _value);
        }

        [Fact]
        public void ItGetsEnvironmentVariableFromDictionary()
        {
            var values = _provider.GetValues();

            Assert.NotNull(values[_key]);
            Assert.Equal(_value, values[_key]);
        }

        [Fact]
        public void IfValueDoesntExistItIsNotPresent()
        {
            var values = _provider.GetValues();
            var notFound = Guid.NewGuid().ToString("N");

            Assert.Throws<KeyNotFoundException>(() => values[notFound]);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(_key, null);
        }
    }
}