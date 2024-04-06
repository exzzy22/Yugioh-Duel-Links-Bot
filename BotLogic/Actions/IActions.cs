using System.Drawing;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickMiddleOfScreen();
    public void ClickDuelist(Point point);
    public void ClickDuelistDialogUntilDissapers();
    public void StartAutoDuel();
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<Point> GetAllWorldDuelistsOnScreen();
    /// <summary>
    /// Click all dialogs that appear after a duel until home screen appears
    /// </summary>
    /// <returns></returns>
    public void ClickAfterDuelDialogs();
    public bool IsOnHomepage();
    public bool IsDuelOver();
}
