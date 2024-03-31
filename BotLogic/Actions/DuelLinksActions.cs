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

    public List<Point> GetAllAvalivableDuelistsOnScreen(List<string> duelists)
    {
        List<Point> points = new();

        foreach (var duelist in duelists)
        { 
            Point? point = _imageFinder.GetImageLocation(duelist, ProcessNames.DUEL_LINKS);

            if (point.HasValue)
            { 
                points.Add(point.Value);
            }
        }

        return points;
    }
}
