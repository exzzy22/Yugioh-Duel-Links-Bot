using System.Drawing;

namespace BotLogic.Actions;

public interface IActions
{
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<Point> GetAllAvalivableDuelistsOnScreen(List<string> duelists);
}
