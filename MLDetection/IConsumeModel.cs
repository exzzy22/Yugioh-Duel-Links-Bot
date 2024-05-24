using MLDetection.Models;

namespace MLDetection;

public interface IConsumeModel
{
    List<ObjectPoint> GetObjects(string imagePath, float threshold, List<Tag>? tags = null);
    List<ObjectPoint> GetObjects(string imagePath, Tag tag, float threshold);
}
