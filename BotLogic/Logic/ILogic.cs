﻿namespace BotLogic.Logic;

public interface ILogic
{
    Task StartDuelWorldLoop(CancellationToken cancellationToken, List<string> duelistTypes);
    Task StartNetworkInterruptionChecker(CancellationToken cts);
}
