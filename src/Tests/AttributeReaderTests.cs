using Xunit;

public class AttributeReaderTests
{
    [Fact]
    public void TimestampAttribute() =>
        Assert.NotNull(AttributeReader.TimestampAttribute);
}