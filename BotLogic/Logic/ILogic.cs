using MLDetection;

namespace BotLogic.Logic;

public interface ILogic
{
    Task StartDuelWorldLoop(CancellationToken cancellationToken, List<Tag> duelistTypes);
    Task StartEventDueldLoop(CancellationToken cancellationToken);
}
