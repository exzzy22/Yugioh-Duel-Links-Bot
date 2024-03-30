using BotLogic.MouseSimulator;
using Emgu.CV;
using Emgu.CV.CvEnum;
using ScreenCapture;
using ScreenCapture.Helpers;
using System.Drawing;
using System.Reflection;

namespace BotLogic.ImageFinder;

public class ImageFinder : IImageFinder
{
    public const string IMAGES_FOLDER_NAME = "Images";

    private readonly IMouseSimulator _mouseSimulator;
    private readonly IScreenCapturer _screenCapturer;
    private readonly IHelpers _helpers;

    public ImageFinder(IMouseSimulator mouseSimulator, IScreenCapturer screenCapturer, IHelpers helpers)
    {
        _mouseSimulator = mouseSimulator;
        _screenCapturer = screenCapturer;
        _helpers = helpers;
    }
    public bool ClickOnImageInWindow(string imageName, string processName)
    {
        Image? screenShoot = _screenCapturer.GetBitmapScreenshot(processName);

        Assembly? assembly = Assembly.GetEntryAssembly();

        if (screenShoot is null || assembly is null)
            return false;

        string? assemblyPath = Path.GetDirectoryName(assembly.Location);

        if (assemblyPath is null)
            return false;

        string imagePath = Path.Combine(assemblyPath, IMAGES_FOLDER_NAME, imageName);

        // Save the screenshot temporarily
        string tempScreenshotPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png"; 
        screenShoot.Save(tempScreenshotPath);

        // Capture the screen to obtain an image of the specific open window
        using (Mat screenCapture = new ())
        {
            // Capture the screen
            CvInvoke.Imread(tempScreenshotPath, ImreadModes.AnyColor).CopyTo(screenCapture); // Or you can use CvInvoke.VideoCapture() to capture from a webcam

            // Load the template image
            Mat template = CvInvoke.Imread(imagePath, ImreadModes.AnyColor);

            // Create a matrix to store the result of the match
            Mat result = new ();

            // Perform template matching
            CvInvoke.MatchTemplate(screenCapture, template, result, TemplateMatchingType.CcoeffNormed);

            // Find the location of the best match
            double minValue = 0;
            double maxValue = 0;
            Point minLoc = new ();
            Point maxLoc = new ();
            CvInvoke.MinMaxLoc(result, ref minValue, ref maxValue, ref minLoc, ref maxLoc);

            // If the minimum value is greater than a certain threshold, consider it a match
            const double threshold = 0.8;
            if (maxValue >= threshold)
            {
                // Simulate a mouse click on the target area
                _mouseSimulator.SimulateMouseClick(maxLoc, _helpers.GetWindowHandle(processName));
                File.Delete(tempScreenshotPath);
                return true;
            }
            else
            {
                // Clean up temporary files
                File.Delete(tempScreenshotPath);
                return false;
            }
        }
    }
}
