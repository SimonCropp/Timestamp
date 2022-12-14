namespace Timestamp;

[AttributeUsage(AttributeTargets.Assembly)]
sealed class TimestampAttribute : Attribute
{
    public string Timestamp { get; private set; }

    public TimestampAttribute(string timestamp) =>
        Timestamp = timestamp;
}