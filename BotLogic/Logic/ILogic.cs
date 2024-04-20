using MLDetection;

namespace BotLogic.Logic;

public interface ILogic
{
    void StartDuelWorldLoop(CancellationToken cancellationToken, List<Tag> duelistTypes);
    void StartEventDueldLoop(CancellationToken cancellationToken);
}
