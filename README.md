Timestamp
=========

Adds a build timestamp to an assembly.

### What's wrong with using Modified or Created date

File timestamps are not reliable since they can changed by various mechanisms, for example when being transferred over the wire.

## What it actually does

Note that this is done at compile time and you will not see the code in your project.

### Adds the following class to your assembly

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

### Adds the following attribute to your assembly

    [assembly: Timestamp("yyyy-MM-dd")]

So for example if you compile your assembly on the 20th of November 2013 the timestamp will be.

    [assembly: Timestamp("2013-11-20")]

### It is UTC

Note that the code that generates the timestamp uses UTC. 

    DateTime.UtcNow.ToString("yyyy-MM-dd")

## How do I access the value

In standard .net you can use the following

    public static string RetrieveTimestamp()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(false)
            .First(x => x.GetType().Name == "TimestampAttribute");

        return (string) attribute.GetType()
            .GetProperty("Timestamp")
            .GetValue(attribute);
    }


If you are on .net 4.5 you can make use of the [AttributeType](http://msdn.microsoft.com/en-us/library/system.reflection.customattributedata.attributetype.aspx) property use the below code. It will be a little faster due to less reflection.


    public static string RetrieveTimestamp()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributesData()
            .First(x => x.AttributeType.Name == "TimestampAttribute");

        return (string)attribute.ConstructorArguments.First().Value;
    }

### What if I need a `DateTime` 

    public static DateTime RetrieveTimestampAsDateTime()
    {
        var timestamp = RetrieveTimestamp();
        return DateTime.ParseExact(timestamp, "yyyy-MM-dd", null, DateTimeStyles.AssumeUniversal)
            .ToUniversalTime();
    }

## How do I test that it has worked

Since all this happens at compile time, with no code seen by you, it is advisable to add a unit test to verify that the injection has happened.

    [Test]
    public void EnsureTimestampHasBeenAdded()
    {
        var timestamp = RetrieveTimestamp();
        Assert.AreEqual(DateTime.UtcNow.ToString("yyyy-MM-dd"), timestamp);
    }

Note that this test makes the assumption that the test is being run on the same day as the compilation of the target assembly. Feel free to change the tolerance of that assumption.

## Nuget 

Nuget package http://nuget.org/packages/Timestamp 

To Install from the Nuget Package Manager Console 
    
    PM> Install-Package Timestamp

## Icon

<a href="http://thenounproject.com/noun/mayan-calendar/#icon-No10219" target="_blank">Mayan Calendar</a> designed by <a href="http://thenounproject.com/Aleks1416" target="_blank">Arturo Alejandro Romo Escartin</a> from The Noun Project

