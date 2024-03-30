using System.Drawing;

namespace BotLogic.MouseSimulator;

public class MouseSimulator : IMouseSimulator
{

    public void SimulateMouseClick(Point point)
    {
        User32.MoveCursorToPoint(point.X, point.Y);

        User32.DoMouseClick();
    }
}
