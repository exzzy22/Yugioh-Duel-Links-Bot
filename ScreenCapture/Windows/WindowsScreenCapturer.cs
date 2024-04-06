using ScreenCapture.Helpers;

namespace ScreenCapture.Windows;

public class WindowsScreenCapturer : IScreenCapturer
{
    private readonly IHelpers _helpers;

    public WindowsScreenCapturer(IHelpers helpers)
    {
        _helpers = helpers;
    }
    public Image GetBitmapScreenshot(string processName)
    {
        nint handle = _helpers.GetWindowHandle(processName);

        //Check if window is minimized and show it if needed
        if (User32.IsIconic(handle))
            User32.ShowWindowAsync(handle, User32.SHOWNORMAL);

        User32.SetForegroundWindow(handle);

        Thread.Sleep(500); // Give time OS to bring screen to front

        if (Screen.PrimaryScreen is null) throw new ArgumentException("Primary screen is null");

        // Create a new bitmap object that matches the size of the screen
        var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                       Screen.PrimaryScreen.Bounds.Height);

        // Create a graphics object from the bitmap
        var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

        // Take a screenshot of the entire screen
        gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0,
                                     0,
                                     Screen.PrimaryScreen.Bounds.Size,
                                     CopyPixelOperation.SourceCopy);

        return bmpScreenshot;
    }
}
