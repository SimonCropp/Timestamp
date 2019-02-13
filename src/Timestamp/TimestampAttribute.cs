namespace Timestamp
{
    [System.AttributeUsage(System.AttributeTargets.Assembly)]
    sealed class TimestampAttribute : System.Attribute
    {
        public string Timestamp { get; }

        public TimestampAttribute(string timestamp)
        {
            Timestamp = timestamp;
        }
    }
}