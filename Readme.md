# SimpleConf
### Configuration made simple!
[![Build status](https://ci.appveyor.com/api/projects/status/kyov4ki5guvt0qid?svg=true)](https://ci.appveyor.com/project/CristianPrieto/configurator) 

How many times do you have to write a line of code like this?
```csharp
var portString = 
	Environment.GetEnvironmentVariable("port");
	
portString = string.IsNullOrEmpty(portString) 
	? ConfigurationManager.AppSettings["port"] 
	: portString;

int port;
if (!Int32.TryParse(portString, out port)) {
	throw new Exception("No port, no bananas!");
}
```

_(Well, maybe not many times but hey, I hope you got the point!)_


What if I can use something as simple as this:

```csharp
public interface ISampleConfiguration {
    int Port { get; }
}

// yada yada yada

var configuration = 
    new ConfigurationBuilder<ISampleConfiguration>()
	    .FromAppSettings()
	    .FromEnvironment()
	    .Build();
```

Now you can set the value in your AppSettings section in your configuration file or setting the value in your environment variables.

Heavily inspired by [this blog post](http://kozmic.net/2014/03/22/strongly-typed-app-settings-with-castle-dictionaryadapter/) from [Krzysztof Koźmic](https://twitter.com/kkozmic).
