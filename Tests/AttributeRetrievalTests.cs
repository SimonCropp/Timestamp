using System.Linq;
using System.Reflection;
using NUnit.Framework;

[TestFixture]
public class AttributeRetrievalTests
{
    [Test]
    public void DotNet4()
    {
        var attribute = (dynamic)Assembly.GetExecutingAssembly()
            .GetCustomAttributes(false)
            .First(x => x.GetType().Name == "TimestampAttribute");

        string timestamp = attribute.Timestamp;
        Assert.AreEqual("2007-04-30", timestamp);
    }

    [Test]
    public void DotNet4_5()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributesData()
            .First(x => x.AttributeType.Name == "TimestampAttribute");

        var timestamp = (string)attribute.ConstructorArguments.First().Value;
        Assert.AreEqual("2007-04-30", timestamp);
    }
}