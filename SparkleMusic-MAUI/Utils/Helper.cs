namespace SparkleMusic_MAUI.Utils;

public class Helper
{
    public static string ConvertToTimeStringFromMilliseconds(double milliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        string timeString = $"{(int)timeSpan.TotalMinutes:D2}:{(int)timeSpan.Seconds:D2}";
        return timeString;
    }

    public static string GetRandomThumbnail()
    {
        Random rand = new Random();
        int randomNumber = rand.Next(1, 13); // Generates a number between 1 and 12
        return $"t{randomNumber}.webp";

    }
}