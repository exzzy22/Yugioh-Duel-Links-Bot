using System.Drawing;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickScreen();
    public bool ClickDuelist(Point point);
    public void ClickDuelistDialogUntilDissapers();
    public void StartAutoDuel();
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<Point> GetAllWorldDuelistsOnScreen(List<string> duelistTypes);
    /// <summary>
    /// Click all dialogs that appear after a duel until home screen appears
    /// </summary>
    /// <returns></returns>
    public void ClickAfterDuelDialogs();
    public bool IsOnHomepage();
    public bool IsDuelOver();
    public Task StartNetworkInterruptionChecker(CancellationToken cts);
}
