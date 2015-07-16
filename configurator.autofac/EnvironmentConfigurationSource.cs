using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace configurator.autofac
{
    public class EnvironmentConfigurationSource : IConfigurationSource
    {
        private readonly Lazy<IDictionary> _environment = 
            new Lazy<IDictionary>(Environment.GetEnvironmentVariables);

        public IDictionary<string, string> GetValues()
        {
            var dict = _environment.Value;
            var env = dict.Keys.Cast<string>()
                .ToDictionary(key => key, key => dict[key] as string);
            return new Dictionary<string, string>(env, StringComparer.OrdinalIgnoreCase);
        }
    }
}