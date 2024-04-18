using System.Runtime.InteropServices;

namespace ScreenCapture.Windows;

internal class User32
{
    public const int SHOWNORMAL = 1;
    public const int SHOWMINIMIZED = 2;
    public const int SHOWMAXIMIZED = 3;

    public static Point GetHandleOffsetFromScreen(IntPtr hWnd)
    {
        Point offsetPoint = new(0, 0);
        ClientToScreen(hWnd, ref offsetPoint);

        return offsetPoint;
    }

    public static Rectangle GetWindowsRectangle(IntPtr hWnd)
    {
        RECT rect;
        GetWindowRect(hWnd, out rect);

        Point offsetPoint = new(0, 0);
        ClientToScreen(hWnd, ref offsetPoint);

        int differenceX = offsetPoint.X - rect.Left;
        int differenceY = offsetPoint.Y - rect.Top;

        int bottomX = rect.Right - rect.Left - differenceX;
        int bottomY = rect.Bottom - rect.Top - differenceY;

        return new Rectangle(offsetPoint.X, offsetPoint.Y, bottomX, bottomY);
    }

    [DllImport("user32.dll")]
    public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);


    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    public static extern bool EmptyClipboard();

    [DllImport("user32.dll")]
    public static extern IntPtr GetOpenClipboardWindow();

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

    [DllImport("user32.dll")]
    public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
}
