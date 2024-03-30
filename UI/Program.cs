using BotLogic.Actions;
using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScreenCapture;
using ScreenCapture.Windows;

namespace UI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        Application.Run(ServiceProvider.GetRequiredService<MainForm>());
    }

    public static IServiceProvider ServiceProvider { get; private set; } = null!;
    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddTransient<IActions, DuelLinksActions>();
                services.AddTransient<IImageFinder, ImageFinder>();
                services.AddTransient<IMouseSimulator, MouseSimulator>();
                services.AddTransient<IScreenCapturer, WindowsScreenCapturer>();
                services.AddTransient<MainForm>();
            });
    }
}