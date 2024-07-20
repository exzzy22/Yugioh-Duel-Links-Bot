using MLDetection;
using MLDetection.Models;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickScreen();
    public bool StartDuel(ObjectPoint objectPoint);
    public void OpenGateDuel();
    public void ClickDuelistDialogUntilDissapers();
    public void StartAutoDuel();
    public void MoveScreenRight();
    public void MoveScreenLeft();
    List<ObjectPoint> GetAllWorldDuelistsOnScreen(List<Tag> duelistTypes);
    public void ClickPopUpDialogs(Func<bool> checkHomepage, CancellationToken cancellationToken);
    public bool IsOnHomepage();
    public bool DoesAssistButtonExists();
    public bool IsDuelOver();
    public bool DoesTagExists(Tag tag, float score = 0f);
    public bool CheckForNetworkInterruption();
    public bool ChangeWorld(Tag world);
    public ObjectPoint OpenDuelistRoadDuel();
    public void OpenGate();
    public bool DoesGateExists();
    public bool DoesStartButtonExists();
    ObjectPoint OpenTagDuel();
    bool DoesTagDuelButtonExists();
}
