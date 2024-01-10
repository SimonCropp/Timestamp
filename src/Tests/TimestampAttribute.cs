namespace Timestamp;

[AttributeUsage(AttributeTargets.Assembly)]
sealed class TimestampAttribute(string timestamp) :
    Attribute
{
    public string Timestamp { get; private set; } = timestamp;
}