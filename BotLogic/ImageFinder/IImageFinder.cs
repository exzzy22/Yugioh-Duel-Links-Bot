using MLDetection;
using MLDetection.Models;
using System.Drawing;

namespace BotLogic.ImageFinder;

public interface IImageFinder
{
    List<ObjectPoint> GetImagesLocationsML(string processName, List<Tag>? tags = null, float threshold = 0.5f);
    List<ObjectPoint> GetImagesLocationsML(string processName, Tag tag, float threshold = 0.5f);
    bool DoesImageExistssML(string processName, Tag tag, float threshold = 0.5f);
    Point? GetImageLocationCV(string imageName, string processName);
    bool DoesImageExistsCV(string imageName, string processName);
    Point GetImagePosition(string processName, ImageAlignment alignment);
}