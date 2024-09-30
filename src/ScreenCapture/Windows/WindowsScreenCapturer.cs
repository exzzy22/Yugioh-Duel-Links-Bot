using ScreenCapture.Helpers;

namespace ScreenCapture.Windows;

public class WindowsScreenCapturer : IScreenCapturer
{
    private readonly IHelpers _helpers;

    public WindowsScreenCapturer(IHelpers helpers)
    {
        _helpers = helpers;
    }
    public Image GetScreenScreenshot(string processName, bool isWindow)
    {
        nint handle = _helpers.GetWindowHandle(processName);

        //Check if window is minimized and show it if needed
        if (User32.IsIconic(handle))
            User32.ShowWindowAsync(handle, User32.SHOWNORMAL);

        User32.SetForegroundWindow(handle);

        Thread.Sleep(500); // Give time OS to bring screen to front

        if (Screen.PrimaryScreen is null) throw new ArgumentException("Primary screen is null");


        if (isWindow)
        {
            var rect = User32.GetWindowsRectangle(handle);

            // Create a new bitmap object that matches the size of the screen
            var bmpWindowsScreenshot = new Bitmap(rect.Width, rect.Height);

            // Create a graphics object from the bitmap
            var gfxWindowScreenshot = Graphics.FromImage(bmpWindowsScreenshot);

            gfxWindowScreenshot.CopyFromScreen(rect.X,
                             rect.Y,
                             0,
                             0,
                             new Size(rect.Width, rect.Height),
                             CopyPixelOperation.SourceCopy);

            return bmpWindowsScreenshot;

        }

        // Create a new bitmap object that matches the size of the screen
        var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                       Screen.PrimaryScreen.Bounds.Height);

        // Create a graphics object from the bitmap
        var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

        gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0,
                                     0,
                                     Screen.PrimaryScreen.Bounds.Size,
                                     CopyPixelOperation.SourceCopy);
        return bmpScreenshot;
    }
}
