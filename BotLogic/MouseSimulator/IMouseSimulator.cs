using System.Drawing;

namespace BotLogic.MouseSimulator;

public interface IMouseSimulator
{
    void SimulateMouseClick(Point point, IntPtr? hWnd = null);

    void DoMouseScroll(int delta);
}
