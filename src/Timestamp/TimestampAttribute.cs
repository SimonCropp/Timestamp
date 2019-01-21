namespace Timestamp
{
    [System.AttributeUsage(System.AttributeTargets.Assembly)]
    class TimestampAttribute : System.Attribute
    {
        public string Timestamp { get; }

        public TimestampAttribute(string timestamp)
        {
            Timestamp = timestamp;
        }
    }
}