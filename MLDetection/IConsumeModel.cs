using MLDetection.Models;

namespace MLDetection;

public interface IConsumeModel
{
    List<ObjectPoint> GetObjects(string imagePath, List<Tag>? tags = null, float threshold = 0f);
    List<ObjectPoint> GetObjects(string imagePath, Tag tag, float threshold = 0f);
}
