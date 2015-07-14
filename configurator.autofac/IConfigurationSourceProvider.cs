using System.Collections.Generic;

namespace configurator.autofac
{
    public interface IConfigurationSourceProvider
    {
        IDictionary<string, string> GetValues();
    }
}