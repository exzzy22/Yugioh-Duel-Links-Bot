using MLDetection;
using MLDetection.Models;
using System.Drawing;

namespace BotLogic.ImageFinder;

public interface IImageFinder
{
    List<ObjectPoint> GetImagesLocationsML(string processName, List<Tag>? tags = null);
    List<ObjectPoint> GetImagesLocationsML(string processName, Tag tag);
    Point? GetImageLocationCV(string imageName, string processName);
    bool DoesImageExistsCV(string imageName, string processName);
    Point GetImagePosition(string processName, ImageAlignment alignment);
}