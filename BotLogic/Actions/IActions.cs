using BotLogic.Models;
using System.Drawing;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickDuelist(Point point);
    public void ClickDuelistDialogUntilDissapers();
    public bool StartAutoDuel();
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<DuelistPoint> GetAllAvalivableDuelistsOnScreen();
}
