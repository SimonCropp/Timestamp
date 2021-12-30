public static class AttributeReader
{
    public static string TimestampAttribute;

    static AttributeReader()
    {
        var assembly = typeof(AttributeReader).Assembly;
        using (var stream = assembly.GetManifestResourceStream("Timestamp.TimestampAttribute.cs"))
        using (var reader = new StreamReader(stream))
        {
            TimestampAttribute = reader.ReadToEnd();
        }
    }
}