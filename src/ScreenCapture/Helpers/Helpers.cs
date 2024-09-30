using System.Diagnostics;

namespace ScreenCapture.Helpers;

public class Helpers : IHelpers
{
    public List<string> GetAllWindowHandleNames()
    {
        List<string> windowHandleNames = new();
        foreach (Process window in Process.GetProcesses())
        {
            window.Refresh();
            if (window.MainWindowHandle != IntPtr.Zero && !string.IsNullOrEmpty(window.MainWindowTitle))
                windowHandleNames.Add(window.ProcessName);
        }
        return windowHandleNames;
    }

    public IntPtr GetWindowHandle(string name)
    {
        var process = Process.GetProcessesByName(name).FirstOrDefault();
        if (process != null && process.MainWindowHandle != IntPtr.Zero)
            return process.MainWindowHandle;

        return IntPtr.Zero;
    }
}
