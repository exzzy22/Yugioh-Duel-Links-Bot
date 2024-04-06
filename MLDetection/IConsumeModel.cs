using MLDetection.Models;

namespace MLDetection;

public interface IConsumeModel
{
    List<ObjectPoint> GetObjects(string imagePath);
}
