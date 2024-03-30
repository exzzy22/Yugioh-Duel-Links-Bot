using BotLogic.ImageFinder;

namespace BotLogic.Actions;

public class DuelLinksActions : IActions
{
    private readonly IImageFinder _imageFinder;

    public DuelLinksActions(IImageFinder imageFinder)
    {
        _imageFinder = imageFinder;
    }

    public void MoveScreenLeft() => _imageFinder.ClickOnImageInWindow(ImageNames.LEFT_ARROW, ProcessNames.DUEL_LINKS);

    public void MoveScreenRight() => _imageFinder.ClickOnImageInWindow(ImageNames.RIGHT_ARROW, ProcessNames.DUEL_LINKS);
}
