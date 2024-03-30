namespace ScreenCapture.Helpers;

public interface IHelpers
{
    List<string> GetAllWindowHandleNames();

    IntPtr GetWindowHandle(string name);
}
