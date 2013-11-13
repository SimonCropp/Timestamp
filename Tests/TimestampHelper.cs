using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

public class TimestampHelper
{
    public static string RetrieveTimestamp()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(false)
            .First(x => x.GetType().Name == "TimestampAttribute");

        return (string) attribute.GetType()
            .GetProperty("Timestamp")
            .GetValue(attribute);
    }

    public static string RetrieveTimestamp45()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributesData()
            .First(x => x.AttributeType.Name == "TimestampAttribute");

        return (string)attribute.ConstructorArguments.First().Value;
    }

    public static DateTime RetrieveTimestampAsDateTime()
    {
        var timestamp = RetrieveTimestamp();
        return DateTime.ParseExact(timestamp, "yyyy-MM-dd", null, DateTimeStyles.AssumeUniversal)
            .ToUniversalTime();
    }


    [Test]
    public void EnsureTimestampHasBeenAdded()
    {
        var timestamp = RetrieveTimestamp();
        Assert.AreEqual(DateTime.UtcNow.ToString("yyyy-MM-dd"), timestamp);
    }
}