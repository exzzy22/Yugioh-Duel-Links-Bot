using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.Logging;
using MLDetection;
using ScreenCapture.Helpers;
using System.Drawing;
using System.Windows.Forms;

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

    public void ClickDuelist(Point point)
    {
        _mouseSimulator.SimulateMouseClick(point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void ClickDuelistDialogUntilDissapers()
    {
        _logger.LogInformation(nameof(ClickDuelistDialogUntilDissapers));

        Point? dialogLocation = _imageFinder.GetImageLocationCV(ImageNames.DIALOG_NEXT, ProcessNames.DUEL_LINKS);

        while (dialogLocation.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(dialogLocation.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(6000);
            dialogLocation = _imageFinder.GetImageLocationCV(ImageNames.DIALOG_NEXT, ProcessNames.DUEL_LINKS);
        }
    }

    public void StartAutoDuel()
    { 
        _logger.LogInformation(nameof(StartAutoDuel));

        Point? autoDialog = _imageFinder.GetImageLocationCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);

        while (!autoDialog.HasValue)
        {
            autoDialog = _imageFinder.GetImageLocationCV(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(autoDialog.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void MoveScreenLeft()
    {
        _logger.LogInformation(nameof(MoveScreenLeft));

        Point? point = _imageFinder.GetImageLocationCV(ImageNames.LEFT_ARROW, ProcessNames.DUEL_LINKS);

        while(!point.HasValue)
        {
            point = _imageFinder.GetImageLocationCV(ImageNames.LEFT_ARROW, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void MoveScreenRight()
    {
        _logger.LogInformation(nameof(MoveScreenRight));

        Point? point = _imageFinder.GetImageLocationCV(ImageNames.RIGHT_ARROW, ProcessNames.DUEL_LINKS);

        while (!point.HasValue)
        {
            point = _imageFinder.GetImageLocationCV(ImageNames.RIGHT_ARROW, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public List<Point> GetAllWorldDuelistsOnScreen(List<string> duelistTypes)
    {
        _logger.LogInformation("Get all world duelists on screen");

        List<Point> worldDuelists = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS)
            .Where(i => duelistTypes.Contains(i.Tag))
            .Select(i => i.Point)
            .ToList();

        return worldDuelists;
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
                ClickMiddleOfScreen();
                if (lastValidClickLocation.HasValue)
                {
                    _mouseSimulator.SimulateMouseClick(lastValidClickLocation.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
                }
                loopcount = 0;
            }

            foreach (var image in ImageNames.MatchOverImages())
            {
                Point? point = _imageFinder.GetImageLocationCV(image, ProcessNames.DUEL_LINKS);

                if (point.HasValue)
                {
                    lastValidClickLocation = point;
                    _logger.LogInformation($"Clicking {image}");
                    _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
                }

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

        return _imageFinder.DoesImageExistsCV(ImageNames.MATCHOVER_OK, ProcessNames.DUEL_LINKS);
    }

    public void ClickMiddleOfScreen()
    {
        _logger.LogInformation(nameof(ClickMiddleOfScreen));

        Point point = _imageFinder.GetImageCenter(ProcessNames.DUEL_LINKS);

        _mouseSimulator.SimulateMouseClick(point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }
}
