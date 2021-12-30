﻿public static class TempFileTracker
{
    public static string TempPath;

    static TempFileTracker()
    {
        TempPath = Path.Combine(Path.GetTempPath(), "Timestamp");
        Directory.CreateDirectory(TempPath);
    }

    public static void DeleteTempFiles()
    {
        foreach (var file in Directory.GetFiles(TempPath))
        {
            if (File.GetLastWriteTime(file) < DateTime.Now.AddDays(-1))
            {
                File.Delete(file);
            }
        }
    }
}