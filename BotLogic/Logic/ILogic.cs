using MLDetection;

namespace BotLogic.Logic;

public interface ILogic
{
    void StartDuelWorldLoop(CancellationToken cancellationToken, List<Tag> duelistTypes);
    Task StartEventDueldLoop(CancellationToken cancellationToken);
}
