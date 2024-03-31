using Emgu.CV;
using Emgu.CV.CvEnum;
using ScreenCapture;
using System.Drawing;
using System.Reflection;

namespace BotLogic.ImageFinder;

public class ImageFinder : IImageFinder
{
    private const string IMAGES_FOLDER_NAME = "Images";
    private const double THRESHOLD = 0.8;


    private readonly IScreenCapturer _screenCapturer;

    public ImageFinder(IScreenCapturer screenCapturer)
    {
        _screenCapturer = screenCapturer;
    }

    public Point? GetImageLocation(string imageName, string processName)
    {
        Image? screenShoot = _screenCapturer.GetBitmapScreenshot(processName);

        Assembly? assembly = Assembly.GetEntryAssembly();

        if (screenShoot is null || assembly is null)
            return null;

        string? assemblyPath = Path.GetDirectoryName(assembly.Location);

        if (assemblyPath is null)
            return null;

        string imagePath = Path.Combine(assemblyPath, IMAGES_FOLDER_NAME, imageName);

        // Save the screenshot temporarily
        string tempScreenshotPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
        screenShoot.Save(tempScreenshotPath);

        // Capture the screen to obtain an image of the specific open window
        using (Mat screenCapture = new())
        {
            // Capture the screen
            CvInvoke.Imread(tempScreenshotPath, ImreadModes.AnyColor).CopyTo(screenCapture); // Or you can use CvInvoke.VideoCapture() to capture from a webcam

            // Load the template image
            Mat template = CvInvoke.Imread(imagePath, ImreadModes.AnyColor);

            // Create a matrix to store the result of the match
            Mat result = new();

            // Perform template matching
            CvInvoke.MatchTemplate(screenCapture, template, result, TemplateMatchingType.CcoeffNormed);

            // Find the location of the best match
            double minValue = 0;
            double maxValue = 0;
            Point minLoc = new();
            Point maxLoc = new();
            CvInvoke.MinMaxLoc(result, ref minValue, ref maxValue, ref minLoc, ref maxLoc);

            // If the minimum value is greater than a certain threshold, consider it a match
            if (maxValue >= THRESHOLD)
            {
                // Calculate the center of the found image
                Point center = new Point(maxLoc.X + template.Width / 4, maxLoc.Y + template.Height / 4);

                File.Delete(tempScreenshotPath);
                return center;
            }
            else
            {
                // Clean up temporary files
                File.Delete(tempScreenshotPath);
                return null;
            }
        }
    }
}
