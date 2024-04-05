namespace BotLogic.Logic;

public interface ILogic
{
    Task StartDuelWorldLoop(CancellationToken cancellationToken);
}
