using System;
using System.Configuration;
using Xunit;

namespace SimpleConf.Tests
{
    public class NonPrefixedFacts
    {
        public interface INonPrefixed
        {
            string NonPrefixedValue { get; }
        }

        private readonly string _value;
        private readonly ConfiguratorBuilder<INonPrefixed> _builder; 

        public NonPrefixedFacts()
        {
            _builder = new ConfiguratorBuilder<INonPrefixed>()
                .FromAppSettings();

            _value = ConfigurationManager.AppSettings["NonPrefixedValue"];
        }

        [Fact]
        public void ItGetsValue()
        {
            var proxy = _builder.Build();
            Assert.NotNull(proxy);
            Assert.Equal(_value, proxy.NonPrefixedValue);
        }
    }

    public class CaseInsensitiveKeysFacts
    {
        public interface INonPrefixed
        {
            string CaseInsensitiveValue { get; }
        }

        private readonly string _value;
        private readonly ConfiguratorBuilder<INonPrefixed> _builder; 

        public CaseInsensitiveKeysFacts()
        {
            _builder = new ConfiguratorBuilder<INonPrefixed>()
                .FromAppSettings();

            _value = ConfigurationManager.AppSettings["caseinsensitivevalue"];
        }

        [Fact]
        public void ItGetsValue()
        {
            var proxy = _builder.Build();
            Assert.NotNull(proxy);
            Assert.Equal(_value, proxy.CaseInsensitiveValue);
        }
    }
}