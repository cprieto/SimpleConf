using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace configurator.autofac
{
    public class EnvironmentConfigurationSourceProvider : IConfigurationSourceProvider
    {
        private readonly Lazy<IDictionary> _environment = 
            new Lazy<IDictionary>(Environment.GetEnvironmentVariables);

        public IDictionary<string, string> GetValues()
        {
            var dict = _environment.Value;
            return dict.Keys.Cast<string>()
                .ToDictionary(key => key, key => dict[key] as string);
        }
    }
}