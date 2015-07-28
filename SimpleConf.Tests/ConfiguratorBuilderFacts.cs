using System;
using System.Configuration;
using Xunit;

namespace SimpleConf.Tests
{
    public class NonPrefixedFacts
    {
        private readonly ConfiguratorBuilder<INonPrefixed> _builder;
        private readonly string _value;

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

        public interface INonPrefixed
        {
            string NonPrefixedValue { get; }
        }
    }

    public class NonPrefixedCaseInsensitiveKeysFacts
    {
        private readonly ConfiguratorBuilder<INonPrefixed> _builder;
        private readonly string _value;

        public NonPrefixedCaseInsensitiveKeysFacts()
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

        public interface INonPrefixed
        {
            string CaseInsensitiveValue { get; }
        }
    }

    public class NonPrefixedValueDoesNotExist
    {
        private readonly ConfiguratorBuilder<INonPrefixed> _builder;
        private readonly string _value;

        public NonPrefixedValueDoesNotExist()
        {
            _builder = new ConfiguratorBuilder<INonPrefixed>()
                .FromAppSettings();

            _value = ConfigurationManager.AppSettings["valuedoesnotexist"];
        }

        [Fact]
        public void ItFails()
        {
            Assert.Null(_value);
            Assert.Throws<Exception>(() => _builder.Build());
        }

        public interface INonPrefixed
        {
            string ValueDoesNotExist { get; }
        }
    }

    public class PrefixedValueDoesNotExist
    {
        private readonly ConfiguratorBuilder<INonPrefixed> _builder;
        private readonly string _value;

        public PrefixedValueDoesNotExist()
        {
            _builder = new ConfiguratorBuilder<INonPrefixed>()
                .WithKeyPrefix("sample")
                .FromAppSettings();

            _value = ConfigurationManager.AppSettings["sample:valuedoesnotexist"];
        }

        [Fact]
        public void ItFails()
        {
            Assert.Null(_value);
            Assert.Throws<Exception>(() => _builder.Build());
        }

        public interface INonPrefixed
        {
            string ValueDoesNotExist { get; }
        }
    }

    public class PrefixedCaseInsensitiveKeysFacts
    {
        private readonly ConfiguratorBuilder<IPrefixed> _builder;
        private readonly string _value;

        public PrefixedCaseInsensitiveKeysFacts()
        {
            _builder = new ConfiguratorBuilder<IPrefixed>()
                .WithKeyPrefix("SAMPLE")
                .FromAppSettings();

            _value = ConfigurationManager.AppSettings["sample:caseinsensitivevalue"];
        }

        [Fact]
        public void ItGetsValue()
        {
            var proxy = _builder.Build();
            Assert.NotNull(proxy);
            Assert.Equal(_value, proxy.CaseInsensitiveValue);
        }

        public interface IPrefixed
        {
            string CaseInsensitiveValue { get; }
        }
    }

    public class PrefixedFacts
    {
        private readonly ConfiguratorBuilder<IPrefixed> _builder;
        private readonly string _value;

        public PrefixedFacts()
        {
            _builder = new ConfiguratorBuilder<IPrefixed>()
                .WithKeyPrefix("sample")
                .FromAppSettings();

            _value = ConfigurationManager.AppSettings["Sample:PrefixedValue"];
        }

        [Fact]
        public void ItGetsValue()
        {
            var proxy = _builder.Build();
            Assert.NotNull(proxy);
            Assert.Equal(_value, proxy.PrefixedValue);
        }

        public interface IPrefixed
        {
            string PrefixedValue { get; }
        }
    }

    public class OrderMattersFacts : IDisposable
    {
        private readonly ConfiguratorBuilder<IPrefixed> _builder;
        private readonly string _value;

        public OrderMattersFacts()
        {
            Environment.SetEnvironmentVariable("SAMPLE:PREFIXEDVALUE", "fromenvironment");

            _builder = new ConfiguratorBuilder<IPrefixed>()
                .WithKeyPrefix("sample")
                .FromAppSettings()
                .FromEnvironment();

            _value = Environment.GetEnvironmentVariable("SAMPLE:PREFIXEDVALUE");
        }

        [Fact]
        public void ItGetsValue()
        {
            var proxy = _builder.Build();
            Assert.NotNull(proxy);
            Assert.Equal(_value, proxy.PrefixedValue);
        }

        public interface IPrefixed
        {
            string PrefixedValue { get; }
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable("SAMPLE:PREFIXEDVALUE", null);
        }
    }
}