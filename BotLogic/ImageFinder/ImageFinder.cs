using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.Extensions.Logging;
using MLDetection;
using MLDetection.Models;
using ScreenCapture;
using System.Drawing;
using System.Reflection;

namespace BotLogic.ImageFinder;

public class ImageFinder : IImageFinder
{
    private const string IMAGES_FOLDER_NAME = "Images";
    private const double MAX_TRESHOLD = 0.9;
    private const double MIN_TRESHOLD = 0.01;

    private readonly IScreenCapturer _screenCapturer;
    private readonly IConsumeModel _consumeModel;
    private readonly ILogger<ImageFinder> _logger;

    public ImageFinder(IScreenCapturer screenCapturer, IConsumeModel consumeModel, ILogger<ImageFinder> logger)
    {
        _screenCapturer = screenCapturer;
        _consumeModel = consumeModel;
        _logger = logger;
    }

    public List<ObjectPoint> GetImagesLocationsML(string processName, List<Tag>? tags = null, float threshold = 0f)
    {
        string tempScreenshotPath = CreateScreenshot(processName);

        List<ObjectPoint> ocbjectPoints = _consumeModel.GetObjects(tempScreenshotPath, tags, threshold);

        File.Delete(tempScreenshotPath);

        return ocbjectPoints;
    }

    public List<ObjectPoint> GetImagesLocationsML(string processName, Tag tag, float threshold = 0f)
    {
        string tempScreenshotPath = CreateScreenshot(processName);

        List<ObjectPoint> ocbjectPoints = _consumeModel.GetObjects(tempScreenshotPath, tag, threshold);

        File.Delete(tempScreenshotPath);

        return ocbjectPoints;
    }

    public bool DoesImageExistssML(string processName, Tag tag, float threshold = 0f)
    {
        string tempScreenshotPath = CreateScreenshot(processName);

        List<ObjectPoint> ocbjectPoints = _consumeModel.GetObjects(tempScreenshotPath, tag, threshold);

        File.Delete(tempScreenshotPath);

        return ocbjectPoints.Count > 0;
    }

    public Point? GetImageLocationCV(string imageName, string processName)
    {
        string tempScreenshotPath = CreateScreenshot(processName);
        string imagePath = GetImagePath(imageName);

        var (rect, minVal, maxVal) = FindTemplateMatch(tempScreenshotPath, imagePath);

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

    public bool DoesImageExistsCV(string imageName, string processName)
    {
        string tempScreenshotPath = CreateScreenshot(processName);
        string imagePath = GetImagePath(imageName);

        var (_, minVal, maxVal) = FindTemplateMatch(tempScreenshotPath, imagePath);

        if (maxVal >= MAX_TRESHOLD && minVal <= MIN_TRESHOLD)
        {
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

    public Point GetImagePosition(string processName, ImageAlignment alignment)
    {
        string tempScreenshotPath = CreateScreenshot(processName);

        int centerX = 0;
        int centerY = 0;

        using (Bitmap bmp = new Bitmap(tempScreenshotPath))
        {
            int marginOffset = (int)(0.15 * bmp.Height); // 15% offset from margins

            switch (alignment)
            {
                case ImageAlignment.Top:
                    centerX = bmp.Width / 2;
                    centerY = marginOffset;
                    break;
                case ImageAlignment.Middle:
                    centerX = bmp.Width / 2;
                    centerY = bmp.Height / 2;
                    break;
                case ImageAlignment.Bottom:
                    centerX = bmp.Width / 2;
                    centerY = bmp.Height - marginOffset;
                    break;
                default:
                    throw new ArgumentException("Invalid alignment specified", nameof(alignment));
            }
        }

        File.Delete(tempScreenshotPath);
        return new Point(centerX, centerY);
    }

    private (Rectangle rectangle, double minVal, double maxVal) FindTemplateMatch(string tempScreenshotPath, string imagePath)
    {
        Mat img = CvInvoke.Imread(tempScreenshotPath, ImreadModes.Grayscale);
        Mat template = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);
        Size size = template.Size;

        Mat result = new();
        CvInvoke.MatchTemplate(img, template, result, TemplateMatchingType.SqdiffNormed);

        double minVal = 0, maxVal = 0;
        Point minLoc = new(), maxLoc = new();
        CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

        Rectangle rect = new Rectangle(minLoc, size);

        return (rect, minVal, maxVal);
    }

    private string GetImagePath(string imageName)
    {
        Assembly assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Failed to get assembly.");
        string assemblyPath = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Failed to get assembly path.");
        string imagePath = Path.Combine(assemblyPath, IMAGES_FOLDER_NAME, imageName);

        return imagePath; ;
    }

    private string CreateScreenshot(string processName)
    {
        Image screenShoot = _screenCapturer.GetScreenScreenshot(processName, true);

        string tempScreenshotPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
        screenShoot.Save(tempScreenshotPath);

        return tempScreenshotPath;
    }
}
