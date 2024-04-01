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
            Thread.Sleep(8000);
            _actions.ClickDuelistDialogUntilDissapers();
            Thread.Sleep(4000);
            _actions.StartAutoDuel();

            bool isDuelOver = false;

            while (!isDuelOver)
            { 
                Thread.Sleep(15000);
                isDuelOver = _actions.IsDuelOver();
            }

            _actions.ClickAfterDuelDialogs();
        }
    }
}
