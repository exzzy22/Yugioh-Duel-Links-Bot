using System.Drawing;

namespace BotLogic.MouseSimulator;

public class MouseSimulator : IMouseSimulator
{
    public void DoMouseScroll(int delta) => User32.DoMouseScroll(delta);

    public void SimulateMouseClick(Point point, IntPtr? hWnd = null)
    {
        User32.MoveCursorToPoint(point.X, point.Y, hWnd);

        User32.DoMouseClick();
    }
}
