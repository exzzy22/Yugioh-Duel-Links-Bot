using System.Drawing;

namespace BotLogic.Models;

public class DuelistPoint
{
    public required Duelist Duelist { get; set; }

    public required Point Point { get; set; }
}
