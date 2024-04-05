using System.Drawing;

namespace BotLogic.ImageFinder;

public interface IImageFinder
{
    List<Point> GetImagesLocationsML(string tagName, string processName);
    Point? GetImageLocationCV(string imageName, string processName);
    bool DoesImageExistsCV(string imageName, string processName);
}