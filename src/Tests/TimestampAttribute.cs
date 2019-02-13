namespace Timestamp
{
    [System.AttributeUsage(System.AttributeTargets.Assembly)]
    sealed class TimestampAttribute : System.Attribute
    {
        public string Timestamp { get; private set; }

        public TimestampAttribute(string timestamp)
        {
            Timestamp = timestamp;
        }
    }
}