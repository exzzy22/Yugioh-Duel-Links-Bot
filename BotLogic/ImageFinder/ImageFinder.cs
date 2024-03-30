using BotLogic.MouseSimulator;
using Emgu.CV;
using Emgu.CV.CvEnum;
using ScreenCapture;
using System.Drawing;

namespace BotLogic.ImageFinder;

public class ImageFinder : IImageFinder
{
    private readonly IMouseSimulator _mouseSimulator;
    private readonly IScreenCapturer _screenCapturer;

    public ImageFinder(IMouseSimulator mouseSimulator, IScreenCapturer screenCapturer)
    {
        _mouseSimulator = mouseSimulator;
        _screenCapturer = screenCapturer;
    }
    public bool ClickOnImageInWindow(string imagePath)
    {
        Image? screenShoot = _screenCapturer.GetBitmapScreenshot(imagePath);

        if(screenShoot is null)
            return false;

        // Save the screenshot temporarily
        string tempScreenshotPath = Path.GetTempFileName();
        screenShoot.Save(tempScreenshotPath);

        // Capture the screen to obtain an image of the specific open window
        using (Mat screenCapture = new ())
        {
            // Capture the screen
            CvInvoke.Imread("screenshot.png", ImreadModes.AnyColor).CopyTo(screenCapture); // Or you can use CvInvoke.VideoCapture() to capture from a webcam

            // Load the template image
            Mat template = CvInvoke.Imread(imagePath, ImreadModes.AnyColor);

            // Create a matrix to store the result of the match
            Mat result = new Mat();

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
                _mouseSimulator.SimulateMouseClick(minLoc);
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
