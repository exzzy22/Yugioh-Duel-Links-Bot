using BotLogic.Actions;
using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.Logging;
using MLDetection;
using MLDetection.Models;
using System.Drawing;

namespace BotLogic.Logic;

public class Logic : ILogic
{
    private readonly IActions _actions;
    private readonly IImageFinder _imageFinder;
    private readonly IMouseSimulator _mouseSimulator;
    private readonly ILogger<Logic> _logger;

    public Logic(IActions actions, IImageFinder imageFinder, IMouseSimulator mouseSimulatorq, ILogger<Logic> logger)
    {
        _actions = actions;
        _imageFinder = imageFinder; 
        _mouseSimulator = mouseSimulatorq;
        _logger = logger;

    }

    public async Task StartDuelWorldLoop(CancellationToken cancellationToken, List<Tag> duelistTypes)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                List<ObjectPoint>? points = _actions.GetAllWorldDuelistsOnScreen(duelistTypes);

                _logger.LogInformation($"Found {points.Count} Duelists");

                foreach (var point in points)
                {
                    _logger.LogInformation("Click Duelist");
                    bool duelistExists = _actions.StartDuel(point);
                    if (!duelistExists)
                    {
                        continue;
                    }

                    while (!_actions.IsDuelOver())
                    {
                        await Task.Delay(10000, cancellationToken);
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickAfterDuelDialogs();
                }

                _actions.MoveScreenRight();
                await Task.Delay(2000, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }
    }

    public async Task StartEventDueldLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                List<ObjectPoint> assistDuelPoint = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.AssistDuelButton);

                if (assistDuelPoint.Count > 0)
                {
                    _actions.StartDuel(assistDuelPoint.First());
                }
                else 
                {
                    _mouseSimulator.DoMouseScroll(-500);
                    continue;
                }

                await Task.Delay(4000, cancellationToken);
                _actions.ClickDuelistDialogUntilDissapers();
                await Task.Delay(4000, cancellationToken);
                _actions.StartAutoDuel();
                await Task.Delay(2000, cancellationToken);

                while (!_actions.IsDuelOver())
                {
                    await Task.Delay(10000, cancellationToken);
                }

                _logger.LogInformation("Duel Over");

                _actions.ClickAfterDuelDialogs();

            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }
    }

    public async Task StartNetworkInterruptionChecker(CancellationToken cts) => await _actions.StartNetworkInterruptionChecker(cts);
}
