using System.Drawing;

namespace BotLogic.MouseSimulator;

public class MouseSimulator : IMouseSimulator
{

    public void SimulateMouseClick(Point point)
    {
        // Set cursor position
        User32.SetCursorPos(point.X, point.Y);

        // Simulate left mouse button down
        User32.MouseEvent(User32.MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);

        // Simulate left mouse button up
        User32.MouseEvent(User32.MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
    }
}
