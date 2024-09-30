using System.Drawing;

namespace MLDetection.Models;

public class ObjectPoint
{
    public Point Point { get; set; }
    public required Tag Tag { get; set; }

    public required float Score { get; set; }
}