using System.Drawing;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickDuelist(Point point);
    public void ClickDuelistDialogUntilDissapers();
    public void StartAutoDuel();
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<Point> GetAllAvalivableDuelistsOnScreen();
}
