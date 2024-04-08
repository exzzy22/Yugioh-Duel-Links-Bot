using BotLogic.Actions;
using BotLogic.ImageFinder;
using BotLogic.Logic;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MLDetection;
using ScreenCapture;
using ScreenCapture.Helpers;
using ScreenCapture.Windows;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Display;
using Serilog.Sinks.WinForms.Base;

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
            .ConfigureLogging(loggingBuilder => loggingBuilder.AddSerilog(GetLogger(), true))
            .ConfigureServices((context, services) => {
                services.AddTransient<ILogic, Logic>();
                services.AddTransient<IActions, DuelLinksActions>();
                services.AddTransient<IImageFinder, ImageFinder>();
                services.AddTransient<IMouseSimulator, MouseSimulator>();
                services.AddTransient<IScreenCapturer, WindowsScreenCapturer>();
                services.AddTransient<IHelpers, Helpers>();
                services.AddTransient<IConsumeModel, ConsumeModel>();
                services.AddTransient<MainForm>();
                //services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(GetLogger(), true));
            });
    }

    private static Logger GetLogger()
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteToSimpleAndRichTextBox(new MessageTemplateTextFormatter("{Timestamp} [{Level}] {Message} {Exception}\n"))
            .WriteTo.File(Path.Combine("Logs", "log-.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 2)
            .CreateLogger();
    }
}