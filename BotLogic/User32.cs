using System.Drawing;
using System.Runtime.InteropServices;

namespace BotLogic;

internal class User32
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    public static void MoveCursorToPoint(int x, int y, IntPtr hWnd, bool adjustForWindow = true)
    {
        if (!adjustForWindow)
        {
            SetCursorPos(x, y);
            return;
        }

        Point offsetPoint = new(0, 0);
        ClientToScreen(hWnd, ref offsetPoint);
        Point adjustedPoint = new Point(offsetPoint.X + x, offsetPoint.Y + y);
        SetCursorPos(adjustedPoint.X, adjustedPoint.Y);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    private const uint MOUSEEVENTF_WHEEL = 0x0800;

    public static void DoMouseClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    public static void DoMouseScroll(int delta)
    {
        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)delta, (long)UIntPtr.Zero);
    }

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
}