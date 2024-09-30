using MLDetection;
using MLDetection.Models;
using System.Drawing;

namespace BotLogic.ImageFinder;

public interface IImageFinder
{
    List<ObjectPoint> GetImagesLocationsML(CancellationToken ct, string processName, List<Tag>? tags = null, float threshold = 0.5f);
    List<ObjectPoint> GetImagesLocationsML(CancellationToken ct, string processName, Tag tag, float threshold = 0.5f);
    bool DoesImageExistssML(CancellationToken ct, string processName, Tag tag, float threshold = 0.5f);
    Point? GetImageLocationCV(CancellationToken ct, string imageName, string processName);
    bool DoesImageExistsCV(CancellationToken ct, string imageName, string processName);
    Point GetImagePosition(CancellationToken ct, string processName, ImageAlignment alignment);
}