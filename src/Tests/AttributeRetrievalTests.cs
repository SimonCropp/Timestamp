using Xunit;

public class AttributeRetrievalTests
{
    [Fact]
    public void DotNet4()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(false)
            .First(x => x.GetType().Name == "TimestampAttribute");

        var timestamp =  (string) attribute.GetType().GetProperty("Timestamp").GetValue(attribute);
        Assert.Equal("2007-04-30", timestamp);
    }

    [Fact]
    public void DotNet4_5()
    {
        var attribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributesData()
            .First(x => x.AttributeType.Name == "TimestampAttribute");

        var timestamp = (string)attribute.ConstructorArguments.First().Value;
        Assert.Equal("2007-04-30", timestamp);
    }
}