using System;
using System.Collections.Generic;
using System.Configuration;
using Xunit;

namespace configurator.autofac.tests
{
    public class AppSettingsConfigurationSourceProviderFacts
    {
        private const string Key = "test:stuff";
        private readonly AppSettingsConfigurationSource _provider;

        public AppSettingsConfigurationSourceProviderFacts()
        {
            _provider = new AppSettingsConfigurationSource();
        }

        [Fact]
        public void ItGetsConfigurationSetting()
        {
            var values = _provider.GetValues();
            var current = ConfigurationManager.AppSettings[Key];

            Assert.NotNull(values[Key]);
            Assert.Equal(current, values[Key]);
        }

        [Fact]
        public void ItDoesntGetNotPresentValues()
        {
            var values = _provider.GetValues();
            var notFound = Guid.NewGuid().ToString();

            Assert.Throws<KeyNotFoundException>(() => values[notFound]);
        }
    }
}