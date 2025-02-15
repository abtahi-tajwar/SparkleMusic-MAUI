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
        string[] thumbnails = { "music_thumb1.png", "music_thumb2.png", "music_thumb3.png", "music_thumb4.png" };
        
        Random random = new Random();
        int index = random.Next(thumbnails.Length); // Get a random index
        
        return thumbnails[index]; // Return the random thumbnail

    }
}