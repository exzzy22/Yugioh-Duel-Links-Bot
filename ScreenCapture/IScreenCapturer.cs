namespace ScreenCapture;

public interface IScreenCapturer
{
    Image GetBitmapScreenshot(string processName);
}