using System.Drawing;

namespace MLDetection;

public interface IConsumeModel
{
    List<Point> GetObjects(string imagePath, string tagName);
}
