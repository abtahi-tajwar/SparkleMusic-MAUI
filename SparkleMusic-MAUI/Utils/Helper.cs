namespace SparkleMusic_MAUI.Utils;

public class Helper
{
    public static string ConvertToTimeStringFromMilliseconds(double milliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        string timeString = $"{(int)timeSpan.TotalMinutes:D2}:{(int)timeSpan.Seconds:D2}";
        return timeString;
    }
}