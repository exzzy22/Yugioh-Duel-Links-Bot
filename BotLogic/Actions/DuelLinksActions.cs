using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.Logging;
using MLDetection;
using MLDetection.Models;
using ScreenCapture.Helpers;
using System.Drawing;

namespace BotLogic.Actions;

public class DuelLinksActions : IActions
{
    private readonly IImageFinder _imageFinder;
    private readonly IMouseSimulator _mouseSimulator;
    private readonly IHelpers _helpers;
    private readonly ILogger<DuelLinksActions> _logger;

    public DuelLinksActions(IImageFinder imageFinder, IMouseSimulator mouseSimulator, IHelpers helpers, ILogger<DuelLinksActions> logger)
    {
        _imageFinder = imageFinder;
        _mouseSimulator = mouseSimulator;
        _helpers = helpers;
        _logger = logger;
    }

    public bool StartDuel(ObjectPoint objectPoint)
    {
        // Click on duelist
        _mouseSimulator.SimulateMouseClick(objectPoint.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
        Thread.Sleep(3000);

        // Check if auto duel button exists
        Point? autoDuelPoint = _imageFinder.GetImageLocationCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);

        if (autoDuelPoint.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(autoDuelPoint.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

            return true;
        }

        // Check if duelist dialogs exists
        List<ObjectPoint> duelistDialogPoint = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.DuelistDialog);

        while (duelistDialogPoint.Count > 0)
        {
            ObjectPoint? dialog = duelistDialogPoint.FirstOrDefault();

            if (dialog is not null)
            {
                _mouseSimulator.SimulateMouseClick(dialog.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

                Thread.Sleep(2000);
            }

            autoDuelPoint = _imageFinder.GetImageLocationCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);

            if (autoDuelPoint.HasValue)
            {
                _mouseSimulator.SimulateMouseClick(autoDuelPoint.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

                return true;
            }

            duelistDialogPoint = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.DuelistDialog);
        }

        // Check for missclicks and get back if it was missclick
        List<ObjectPoint> missclickButtons = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tags.MissClickButtons());

        ObjectPoint? backButton = missclickButtons.FirstOrDefault();

        if (backButton is not null)
        {
            _mouseSimulator.SimulateMouseClick(backButton.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(2000);
        }

        return false;
    }

    public void ClickDuelistDialogUntilDissapers()
    {
        _logger.LogInformation(nameof(ClickDuelistDialogUntilDissapers));

        List<ObjectPoint> duelistDialogPoints = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.DuelistDialog);

        while (duelistDialogPoints.Count > 0)
        {
            foreach (ObjectPoint point in duelistDialogPoints)
            {
                _mouseSimulator.SimulateMouseClick(point.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            }
            Thread.Sleep(3000);

            if(_imageFinder.DoesImageExistsCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS)) { break; }

            duelistDialogPoints = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.DuelistDialog);
        }
    }

    public void StartAutoDuel()
    { 
        _logger.LogInformation(nameof(StartAutoDuel));

        int retryCount = 0;

        Point? autoDialog = _imageFinder.GetImageLocationCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);

        while (!autoDialog.HasValue)
        {
            if (retryCount > 3) return;

            autoDialog = _imageFinder.GetImageLocationCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);
            retryCount++;
        }

        _mouseSimulator.SimulateMouseClick(autoDialog.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void MoveScreenLeft()
    {
        _logger.LogInformation(nameof(MoveScreenLeft));

        int retryCount = 0;

        Point? point = _imageFinder.GetImageLocationCV(ImageNames.LEFT_ARROW, ProcessNames.DUEL_LINKS);

        while(!point.HasValue)
        {
            if (retryCount > 3) return;

            point = _imageFinder.GetImageLocationCV(ImageNames.LEFT_ARROW, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void MoveScreenRight()
    {
        _logger.LogInformation(nameof(MoveScreenRight));

        int retryCount = 0;

        Point? point = _imageFinder.GetImageLocationCV(ImageNames.RIGHT_ARROW, ProcessNames.DUEL_LINKS);

        while (!point.HasValue)
        {
            if (retryCount > 3) return;

            point = _imageFinder.GetImageLocationCV(ImageNames.RIGHT_ARROW, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public List<ObjectPoint> GetAllWorldDuelistsOnScreen(List<Tag> duelistTypes)
    {
        _logger.LogInformation("Get all world duelists on screen");

        List<ObjectPoint> worldDuelists = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, duelistTypes);

        return worldDuelists
            .Where(i => duelistTypes.Contains(i.Tag))
            .ToList();
    }

    public void ClickAfterDuelDialogs()
    {
        _logger.LogInformation(nameof(ClickAfterDuelDialogs));

        bool homepageexists = false;

        int loopcount = 0;

        while (!homepageexists)
        {
            Point? lastValidClickLocation = null;

            if (loopcount > 3)
            {
                ClickScreen();
                if (lastValidClickLocation.HasValue)
                {
                    _mouseSimulator.SimulateMouseClick(lastValidClickLocation.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
                }
                loopcount = 0;
            }

            List<ObjectPoint> clickableButtons = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tags.ClickableButtons());

            foreach (var buttonPoint in clickableButtons)
            {
                _mouseSimulator.SimulateMouseClick(buttonPoint.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

                Thread.Sleep(500);
            }

            homepageexists = IsOnHomepage();

            loopcount++;
        }
    }

    public bool IsOnHomepage()
    {
        foreach (var image in ImageNames.HomepageImages())
        {
            if (_imageFinder.DoesImageExistsCV(image, ProcessNames.DUEL_LINKS))
            {
                return true;
            }
        }

        _logger.LogInformation("Not on homepage");
        return false;
    }

    public bool IsDuelOver()
    {
        _logger.LogInformation(nameof(IsDuelOver));

        List<ObjectPoint> okButtonPoint = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS);

        return okButtonPoint.Count > 0;
    }

    public void ClickScreen()
    {
        _logger.LogInformation(nameof(ClickScreen));

        Point point = _imageFinder.GetImagePosition(ProcessNames.DUEL_LINKS, ImageAlignment.Bottom);

        _mouseSimulator.SimulateMouseClick(point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public async Task StartNetworkInterruptionChecker(CancellationToken cts)
    {
        PeriodicTimer timer = new (TimeSpan.FromSeconds(10));

        try
        {
            while (await timer.WaitForNextTickAsync(cts) && !cts.IsCancellationRequested)
            {
                List<ObjectPoint> retryPoint = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.RetryButton);

                if (retryPoint.Count > 0)
                {
                    _logger.LogInformation("Click network interruption error popup");
                    _mouseSimulator.SimulateMouseClick(retryPoint.First().Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stoping Network Interruption Checker");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }
        finally
        {
            _logger.LogInformation("Network Interruption Checker Finally");
            timer.Dispose();
        }
    }
}
