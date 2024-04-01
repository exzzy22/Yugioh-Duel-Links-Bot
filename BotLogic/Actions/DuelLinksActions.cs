using BotLogic.ImageFinder;
using BotLogic.Models;
using BotLogic.MouseSimulator;
using ScreenCapture.Helpers;
using System.Drawing;

namespace BotLogic.Actions;

public class DuelLinksActions : IActions
{
    private readonly IImageFinder _imageFinder;
    private readonly IMouseSimulator _mouseSimulator;
    private readonly IHelpers _helpers;

    public DuelLinksActions(IImageFinder imageFinder, IMouseSimulator mouseSimulator, IHelpers helpers)
    {
        _imageFinder = imageFinder;
        _mouseSimulator = mouseSimulator;
        _helpers = helpers;
    }

    public void ClickDuelist(Point point)
    {
        _mouseSimulator.SimulateMouseClick(point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void ClickDuelistDialogUntilDissapers()
    {
        Point? dialogLocation = _imageFinder.GetImageLocation(ImageNames.DIALOG_NEXT, ProcessNames.DUEL_LINKS);

        while (dialogLocation.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(dialogLocation.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(6000);
            dialogLocation = _imageFinder.GetImageLocation(ImageNames.DIALOG_NEXT, ProcessNames.DUEL_LINKS);
        }
    }

    public void StartAutoDuel()
    { 
        Point? autoDialog = _imageFinder.GetImageLocation(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);

        while (!autoDialog.HasValue)
        {
            autoDialog = _imageFinder.GetImageLocation(ImageNames.AUTO_DUEL, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(autoDialog.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void MoveScreenLeft()
    { 
        Point? point = _imageFinder.GetImageLocation(ImageNames.LEFT_ARROW, ProcessNames.DUEL_LINKS);

        if (point.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
        }
    }

    public void MoveScreenRight()
    { 
        Point? point = _imageFinder.GetImageLocation(ImageNames.RIGHT_ARROW, ProcessNames.DUEL_LINKS);

        if (point.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
        }
    }

    public List<DuelistPoint> GetAllAvalivableDuelistsOnScreen()
    {
        List<DuelistPoint> points = [];

        foreach (var duelist in ImageNames.Duelists.GetAllDuelists())
        {
            foreach (var image in duelist.ImagePaths)
            {
                Point? point = _imageFinder.GetImageLocation(image, ProcessNames.DUEL_LINKS);

                if (point.HasValue)
                {
                    points.Add(new DuelistPoint { Duelist = duelist, Point = point.Value});
                    break;
                }
            }
        }
        return points;
    }

    public void ClickAfterDuelDialogs()
    {
        bool homepageexists = false;


        while (!homepageexists)
        {
            foreach (var image in ImageNames.MatchOverImages())
            {
                Point? point = _imageFinder.GetImageLocation(image, ProcessNames.DUEL_LINKS);

                if (point.HasValue)
                {
                    _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
                }

                Thread.Sleep(1000);
            }

            homepageexists = IsOnHomepage();
        }
    }

    public bool IsOnHomepage()
    {
        foreach (var image in ImageNames.HomepageImages())
        {
            if (_imageFinder.DoesImageExists(image, ProcessNames.DUEL_LINKS))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsDuelOver() => _imageFinder.DoesImageExists(ImageNames.MATCHOVER_OK, ProcessNames.DUEL_LINKS);
}
