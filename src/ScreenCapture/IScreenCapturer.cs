namespace ScreenCapture;

public interface IScreenCapturer
{
    Image GetScreenScreenshot(string processName, bool isWindow);
}