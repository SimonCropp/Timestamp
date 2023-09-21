Timestamp
=========

Adds a build timestamp to an assembly.

**See [Milestones](../../milestones?state=closed) for release notes.**


## The NuGet package [![NuGet Status](http://img.shields.io/nuget/v/Timestamp.svg?style=flat)](https://www.nuget.org/packages/Timestamp/)

https://nuget.org/packages/Timestamp/

    PM> Install-Package Timestamp


## What's wrong with using Modified or Created date

File timestamps are not reliable since they can be changed by various mechanisms, for example when being transferred over the wire.


## What it actually does

Note that this is done at compile time and you will not see the code in your project.


### Adds the following class to your assembly

```csharp
namespace Timestamp
{
    [System.AttributeUsage(System.AttributeTargets.Assembly)]
    class TimestampAttribute : System.Attribute
    {
        public string Timestamp { get; private set; }

        public TimestampAttribute(string timestamp)
        {
            Timestamp = timestamp;
        }
    }
}
```


### Adds the following attribute to your assembly

```csharp
[assembly: Timestamp("yyyy-MM-ddTHH:mm:ss.fffZ")]
```

So for example, if you compile your assembly on the 10th of September 2018 the timestamp will be

```csharp
[assembly: Timestamp("2018-09-10T16:24:59.417Z")]
```


### It is UTC

Note that the code that generates the timestamp uses UTC. 

```csharp
DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
```

## How do I access the value

In standard .net you can use the following

```csharp
public static string RetrieveTimestamp()
{
    var attribute = Assembly.GetExecutingAssembly()
        .GetCustomAttributes(false)
        .First(x => x.GetType().Name == "TimestampAttribute");

    return (string) attribute.GetType()
        .GetProperty("Timestamp")
        .GetValue(attribute);
}
```

If you are on .net 4.5 you can make use of the [AttributeType](http://msdn.microsoft.com/en-us/library/system.reflection.customattributedata.attributetype.aspx) property use the below code. It will be a little faster due to less reflection.


```csharp
public static string RetrieveTimestamp()
{
    var attribute = Assembly.GetExecutingAssembly()
        .GetCustomAttributesData()
        .First(x => x.AttributeType.Name == "TimestampAttribute");

    return (string)attribute.ConstructorArguments.First().Value;
}
```


### What if I need a `DateTime` 

```csharp
public static DateTime RetrieveTimestampAsDateTime()
{
    var timestamp = RetrieveTimestamp();
    return DateTime.ParseExact(timestamp, "yyyy-MM-ddTHH:mm:ss.fffZ", null, DateTimeStyles.AssumeUniversal)
        .ToUniversalTime();
}
```


## How do I test that it has worked

Since all this happens at compile time, with no code seen by you, it is advisable to add a unit test to verify that the injection has happened.

```csharp
[Test]
public void EnsureTimestampHasBeenAdded()
{
    var timestamp = RetrieveTimestamp();
    Assert.AreEqual(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), timestamp);
}
```

Note that this test makes the assumption that the test is being run on the same day as the compilation of the target assembly. Feel free to change the tolerance of that assumption.


## Nuget

Nuget package http://nuget.org/packages/Timestamp

To Install from the NuGet Package Manager Console

```powershell
PM> Install-Package Timestamp
```


## Icon

<a href="http://thenounproject.com/noun/mayan-calendar/#icon-No10219" target="_blank">Mayan Calendar</a> designed by <a href="http://thenounproject.com/Aleks1416" target="_blank">Arturo Alejandro Romo Escartin</a> from The Noun Project