using MLDetection;
using MLDetection.Models;

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
    public void ClickPopUpDialogs(Func<bool> checkHomepage);
    public bool IsOnHomepage();
    public bool IsDuelOver();
    public bool DoesTagExists(Tag tag, float score = 0f);
    public bool CheckForNetworkInterruption();
}
