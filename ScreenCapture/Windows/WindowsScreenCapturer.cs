using ScreenCapture.Helpers;

namespace ScreenCapture.Windows;

public class WindowsScreenCapturer : IScreenCapturer
{
    private readonly IHelpers _helpers;

    public WindowsScreenCapturer(IHelpers helpers)
    {
        _helpers = helpers;
    }
    public Image? GetBitmapScreenshot(string processName)
    {
        Image? img = null;

        nint handle = _helpers.GetWindowHandle(processName);

        //Check if window is minimized and show it if needed
        if (User32.IsIconic(handle))
            User32.ShowWindowAsync(handle, User32.SHOWNORMAL);

        User32.SetForegroundWindow(handle);

        //ALT + PRINT SCREEN gets screenshot of focused window
        //See this article for key list
        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=windowsdesktop-6.0#remarks
        SendKeys.SendWait("%({PRTSC})");
        Thread.Sleep(600);

        //The GetImage function in WPF gets a bitmapsource image
        //This could be replaced with the Winforms getimage since that returns an image
        img = Clipboard.GetImage();

        //Uses the user32.dll to make sure the clipboard is empty and closed 
        //Without this you might get errors that the clipboard is already open
        nint clipWindow = User32.GetOpenClipboardWindow();
        User32.OpenClipboard(clipWindow);
        User32.EmptyClipboard();
        User32.CloseClipboard();
        Thread.Sleep(100);

        return img;
    }
}
