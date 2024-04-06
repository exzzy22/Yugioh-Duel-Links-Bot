using BotLogic.Actions;
using Microsoft.Extensions.Logging;

namespace BotLogic.Logic;

public class Logic : ILogic
{
    private readonly IActions _actions;
    private readonly ILogger<Logic> _logger;

    public Logic(IActions actions, ILogger<Logic> logger)
    {
        _actions = actions;
        _logger = logger;

    }

    public async Task StartDuelWorldLoop(CancellationToken cancellationToken , List<string> duelistTypes)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var points = _actions.GetAllWorldDuelistsOnScreen(duelistTypes);

                _logger.LogInformation($"Found {points.Count} Duelists");

                if (!points.Any())
                {
                    _actions.MoveScreenRight();
                    await Task.Delay(4000, cancellationToken);
                    continue;
                }

                foreach (var point in points)
                {
                    _logger.LogInformation("Click Duelist");
                    _actions.ClickDuelist(point);
                    await Task.Delay(4000, cancellationToken);
                    _actions.ClickDuelistDialogUntilDissapers();
                    await Task.Delay(4000, cancellationToken);
                    _actions.StartAutoDuel();
                    await Task.Delay(2000, cancellationToken);

                    if (_actions.IsOnHomepage()) continue;


                    while (!_actions.IsDuelOver())
                    {
                        await Task.Delay(10000, cancellationToken);
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickAfterDuelDialogs();
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }
    }

    public async Task StartNetworkInterruptionChecker(CancellationToken cts) => await _actions.StartNetworkInterruptionChecker(cts);
}
