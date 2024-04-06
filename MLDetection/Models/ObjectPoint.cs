using System.Drawing;

namespace MLDetection.Models;

public class ObjectPoint
{
    public Point Point { get; set; }
    public required string Tag { get; set; }
}