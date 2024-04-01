using Emgu.CV;
using Emgu.CV.CvEnum;
using ScreenCapture;
using System.Drawing;
using System.Reflection;

namespace BotLogic.ImageFinder;

public class ImageFinder : IImageFinder
{
    private const string IMAGES_FOLDER_NAME = "Images";
    private const double MAX_TRESHOLD = 0.9;
    private const double MIN_TRESHOLD = 0.001;

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

        Mat img = CvInvoke.Imread(tempScreenshotPath, ImreadModes.Grayscale);
        Mat template = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);
        Size size = template.Size;

        Mat img2 = img.Clone();

        Mat result = new Mat();
        CvInvoke.MatchTemplate(img2, template, result, TemplateMatchingType.SqdiffNormed);

        double minVal = 0, maxVal = 0;
        Point minLoc = new ();
        Point maxLoc = new ();
        CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

        Rectangle rect = new Rectangle(minLoc, size);

        if (maxVal >= MAX_TRESHOLD && minVal <= MIN_TRESHOLD)
        {
            // Calculate the center of the found image
            Point center = new (rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

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
