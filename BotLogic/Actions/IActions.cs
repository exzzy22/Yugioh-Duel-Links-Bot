using MLDetection;
using MLDetection.Models;

namespace BotLogic.Actions;

public interface IActions
{
    public void ClickScreen(CancellationToken ct);
    public bool StartDuel(CancellationToken ct, ObjectPoint objectPoint);
    public void OpenGateDuel(CancellationToken ct);
    public void ClickDuelistDialogUntilDissapers(CancellationToken ct);
    public void StartAutoDuel(CancellationToken ct);
    public void MoveScreenRight(CancellationToken ct);
    public void MoveScreenLeft(CancellationToken ct);
    List<ObjectPoint> GetAllWorldDuelistsOnScreen(CancellationToken ct, List<Tag> duelistTypes);
    public void ClickPopUpDialogs(Func<bool> checkHomepage, CancellationToken cancellationToken);
    public bool IsOnHomepage(CancellationToken ct);
    public bool DoesAssistButtonExists(CancellationToken ct);
    public bool IsDuelOver(CancellationToken ct);
    public bool DoesTagExists(CancellationToken ct, Tag tag, float score = 0f);
    public bool CheckForNetworkInterruption(CancellationToken ct);
    public bool ChangeWorld(CancellationToken ct,Tag world);
    public ObjectPoint OpenDuelistRoadDuel(CancellationToken ct);
    public void OpenGate(CancellationToken ct);
    public bool DoesGateExists(CancellationToken ct);
    public bool DoesStartButtonExists(CancellationToken ct);
    ObjectPoint OpenTagDuel(CancellationToken ct);
    bool DoesTagDuelButtonExists(CancellationToken ct);
}
