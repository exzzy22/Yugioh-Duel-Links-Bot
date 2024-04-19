using MLDetection;
using MLDetection.Models;
using System.Drawing;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickScreen();
    public bool StartDuel(ObjectPoint objectPoint);
    public void ClickDuelistDialogUntilDissapers();
    public void StartAutoDuel();
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<ObjectPoint> GetAllWorldDuelistsOnScreen(List<Tag> duelistTypes);
    /// <summary>
    /// Click all dialogs that appear after a duel until home screen appears
    /// </summary>
    /// <returns></returns>
    public void ClickAfterDuelDialogs();
    public bool IsOnHomepage();
    public bool IsDuelOver();
    public Task StartNetworkInterruptionChecker(CancellationToken cts);
}
