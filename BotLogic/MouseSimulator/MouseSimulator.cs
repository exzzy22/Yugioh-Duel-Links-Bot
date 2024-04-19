using Microsoft.Extensions.Logging;
using System.Drawing;

namespace BotLogic.MouseSimulator;

public class MouseSimulator : IMouseSimulator
{
    private readonly ILogger<MouseSimulator> _logger;

    public MouseSimulator(ILogger<MouseSimulator> logger)
    {
        _logger = logger;
    }

    public void DoMouseScroll(int delta) => User32.DoMouseScroll(delta);

    public void SimulateMouseClick(Point point, IntPtr hWnd)
    {
        User32.MoveCursorToPoint(point.X, point.Y, hWnd, true);

        _logger.LogInformation($"Click {point.X}, {point.Y}");

        User32.DoMouseClick();
    }
}
