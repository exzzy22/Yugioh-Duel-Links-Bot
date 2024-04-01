using System.Drawing;

namespace BotLogic.ImageFinder;

public interface IImageFinder
{
    Point? GetImageLocation(string imageName, string processName);
    bool DoesImageExists(string imageName, string processName);
}