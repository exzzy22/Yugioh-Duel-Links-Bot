using System.Drawing;

namespace BotLogic.MouseSimulator;

public interface IMouseSimulator
{
    void SimulateMouseClick(Point point, IntPtr hWnd);

    void DoMouseScroll(int delta);
}
