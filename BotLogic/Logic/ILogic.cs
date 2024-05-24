using MLDetection;

namespace BotLogic.Logic;

public interface ILogic
{
    void StartDuelWorldsLoop(CancellationToken cancellationToken, List<Tag> duelistTypes, bool changeWorld);

    void StartDuelWorldLoop(CancellationToken cancellationToken, List<Tag> duelistTypes, bool changeWorld);
    void StartEventDueldLoop(CancellationToken cancellationToken);
    void StartGateLoop(CancellationToken cancellationToken);
}
