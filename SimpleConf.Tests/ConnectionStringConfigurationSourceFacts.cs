using System;
using System.Collections.Generic;
using System.Configuration;
using Xunit;

namespace SimpleConf.Tests
{
    public class ConnectionStringConfigurationSourceFacts
    {
        private const string Name = "test";

        private readonly ConnectionStringsConfigurationSource _provider = 
            new ConnectionStringsConfigurationSource();

        [Fact]
        public void ItGetsConnectionStringFromName()
        {
            var expected = ConfigurationManager.ConnectionStrings[Name].ConnectionString;
            var values = _provider.GetValues();

            Assert.NotNull(values[Name]);
            Assert.Equal(expected, values[Name]);
        }

        [Fact]
        public void IfNotThereIsNotThere()
        {
            var values = _provider.GetValues();
            var notFound = Guid.NewGuid().ToString();

            Assert.Throws<KeyNotFoundException>(() => values[notFound]);
        }
    }
}