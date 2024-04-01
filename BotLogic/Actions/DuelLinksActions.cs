using BotLogic.ImageFinder;
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

        if (autoDialog.HasValue)
        { 
            _mouseSimulator.SimulateMouseClick(autoDialog.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
        }
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

    public List<Point> GetAllAvalivableDuelistsOnScreen()
    {
        List<Point> points = [];

        foreach (var duelist in ImageNames.Duelists.GetAllDuelists())
        {
            foreach (var image in duelist.ImagePaths)
            {
                Point? point = _imageFinder.GetImageLocation(image, ProcessNames.DUEL_LINKS);

                if (point.HasValue)
                {
                    points.Add(point.Value);
                    break;
                }
            }
        }
        return points;
    }
}
