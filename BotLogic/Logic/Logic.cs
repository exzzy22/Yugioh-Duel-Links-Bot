using BotLogic.Actions;

namespace BotLogic.Logic;

public class Logic : ILogic
{
    private readonly IActions _actions;

    public Logic(IActions actions)
    {
        _actions = actions;
    }

    public void StartDuelWorldLoop(CancellationToken cancellationToken)
    {
        var duelists = _actions.GetAllAvalivableDuelistsOnScreen();

        foreach (var duelist in duelists)
        {
            _actions.ClickDuelist(duelist.Point);
            _actions.ClickDuelistDialogUntilDissapers();
            _actions.StartAutoDuel();
        }
    }
}
