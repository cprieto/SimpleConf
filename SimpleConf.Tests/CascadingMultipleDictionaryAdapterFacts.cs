using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleConf.Tests
{
    public class CascadingMultipleDictionaryAdapterFacts
    {
        [Fact]
        public void WithNoInputItJustThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new CascadingMultipleDictionaryAdapter(null));
        }

        [Fact]
        public void EmptyInputIsOk()
        {
            // ReSharper disable once UnusedVariable
            var dummy = new CascadingMultipleDictionaryAdapter(new List<IDictionary<string, string>>());
        }

        [Fact]
        public void DefaultCtorIsOk()
        {
            var adapter = new CascadingMultipleDictionaryAdapter();

            Assert.NotNull(adapter.Sources);
            Assert.Empty(adapter.Sources);
        }

        [Fact]
        public void AddingNullSourceThrows()
        {
            var adapter = new CascadingMultipleDictionaryAdapter();

            Assert.Throws<ArgumentNullException>(() => adapter.AddSource(null));
        }

        [Fact]
        public void AddingEmptySourceIsOk()
        {
            var adapter = new CascadingMultipleDictionaryAdapter();
            adapter.AddSource(new Dictionary<string, string>());

            Assert.NotEmpty(adapter.Sources);
        }

        [Fact]
        public void ItContainsTheKeysOfSourceDictionaries()
        {
            var dict1 = new Dictionary<string, string> { { "item1", "value1" } };
            var dict2 = new Dictionary<string, string> { { "item2", "value2" } };

            var adapter = new CascadingMultipleDictionaryAdapter(new[] { dict1, dict2 });

            Assert.Equal(2, adapter.Sources.Count());
            Assert.True(adapter.Contains("item1"));
            Assert.True(adapter.Contains("item2"));
            Assert.False(adapter.Contains("item3"));
        }

        [Fact]
        public void ItGetsTheValueFomDictionary()
        {
            var dict1 = new Dictionary<string, string> { { "item1", "value1" } };
            var dict2 = new Dictionary<string, string> { { "item2", "value2" } };

            var adapter = new CascadingMultipleDictionaryAdapter(new[] { dict1, dict2 });

            Assert.Equal("value1", adapter["item1"]);
            Assert.Equal("value2", adapter["item2"]);
            Assert.Null(adapter["item3"]);
        }

        [Fact]
        public void OrderAffectValuesWhenTheSameKeyIsPresent()
        {
            var dict1 = new Dictionary<string, string> { { "item1", "value1" } };
            var dict2 = new Dictionary<string, string> { { "item1", "value2" } };

            var adapter = new CascadingMultipleDictionaryAdapter(new[] { dict1, dict2 });

            Assert.Equal("value2", adapter["item1"]);
        }
    }
}