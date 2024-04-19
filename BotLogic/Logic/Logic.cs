using BotLogic.Actions;
using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.Logging;
using MLDetection;
using MLDetection.Models;

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
                List<ObjectPoint> duelistsOnHomepage = _actions.GetAllWorldDuelistsOnScreen(duelistTypes);

                _logger.LogInformation("Found {Count} Duelists", duelistsOnHomepage.Count);

                if (duelistsOnHomepage.Count < 1)
                {
                    _actions.CheckForNetworkInterruption();
                }

                foreach (var point in duelistsOnHomepage)
                {
                    bool duelistExists = _actions.StartDuel(point);
                    _logger.LogInformation("Click {Tag}", point.Tag);

                    if (!duelistExists) continue;

                    int duelCheckCounter = 0;
                    while (!_actions.IsDuelOver())
                    {
                        await Task.Delay(10000, cancellationToken);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption();

                            if(!result) _actions.ClickScreen();
                        }
                        duelCheckCounter++;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs();
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

                _actions.ClickPopUpDialogs();

            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }

    }
}
