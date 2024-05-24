using BotLogic.Actions;
using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.Logging;
using MLDetection;
using MLDetection.Models;
using System.Collections.Immutable;

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

    public void StartDuelWorldsLoop(CancellationToken cancellationToken, List<Tag> duelistTypes, bool changeWorld)
    {
        if (!changeWorld)
        {
            StartDuelWorldLoop(cancellationToken, duelistTypes, changeWorld);

            return;
        }

        ImmutableList<Tag> duelWorlds = Tags.Worlds();
        int currentIndex = 0;
        bool isFirstIteration = true;

        while (true)
        {
            if (!isFirstIteration)
            {
                Tag currentWorld = duelWorlds[currentIndex];
                _logger.LogInformation("Change the world to: {world}", currentWorld);

                bool isChanged = _actions.ChangeWorld(currentWorld);

                if (!isChanged)
                {
                    _logger.LogError("World change failed");
                    bool result = _actions.CheckForNetworkInterruption();
                }

                if (cancellationToken.IsCancellationRequested) break;
            }

            StartDuelWorldLoop(cancellationToken, duelistTypes, changeWorld);

            currentIndex = (currentIndex + 1) % duelWorlds.Count;

            isFirstIteration = false;

            if (cancellationToken.IsCancellationRequested) break;
        }

        _logger.LogInformation("Program stopped");
    }

    public void StartDuelWorldLoop(CancellationToken cancellationToken, List<Tag> duelistTypes, bool changeWorld)
    {
        try
        {
            int homepagesChecked = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                if(changeWorld && homepagesChecked > 4) break;

                List<ObjectPoint> duelistsOnHomepage = _actions.GetAllWorldDuelistsOnScreen(duelistTypes);

                _logger.LogInformation("Found {Count} Duelists", duelistsOnHomepage.Count);

                cancellationToken.ThrowIfCancellationRequested();

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
                        cancellationToken.ThrowIfCancellationRequested();
                        Thread.Sleep(10000);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption();

                            if (!result) _actions.ClickScreen();
                        }
                        duelCheckCounter++;

                        if (cancellationToken.IsCancellationRequested) break;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs(_actions.IsOnHomepage, cancellationToken);
                }

                cancellationToken.ThrowIfCancellationRequested();
                _actions.MoveScreenRight();
                Thread.Sleep(2000);
                homepagesChecked++;

                if (cancellationToken.IsCancellationRequested) break;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }
        finally
        {
            _logger.LogInformation("Stopping program");
        }

        _logger.LogInformation("Program stopped");
    }

    public void StartEventDueldLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                List<ObjectPoint> assistDuelPoint = _imageFinder.GetImagesLocationsML(ProcessNames.DUEL_LINKS, Tag.AssistDuelButton);

                _logger.LogInformation("Found {Count} AssistedDuelButtons", assistDuelPoint.Count);

                if (assistDuelPoint.Count < 1)
                {
                    bool isNetworkInterUpted = _actions.CheckForNetworkInterruption();

                    if (!isNetworkInterUpted)
                    {
                        _actions.ClickScreen();
                        Thread.Sleep(2000);
                        _actions.ClickPopUpDialogs(_actions.DoesAssistButtonExists, cancellationToken);
                    }

                    if(cancellationToken.IsCancellationRequested) break;
                }
                else
                {
                    ObjectPoint buttonPoint = assistDuelPoint.First();
                    bool buttonPressed = _actions.StartDuel(buttonPoint);

                    int duelCheckCounter = 0;
                    while (!_actions.IsDuelOver())
                    {
                        Thread.Sleep(10000);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption();

                            if (!result) _actions.ClickScreen();
                        }
                        duelCheckCounter++;

                        if (cancellationToken.IsCancellationRequested) break;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs(_actions.DoesAssistButtonExists, cancellationToken);

                    Thread.Sleep(2000);
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }

        _logger.LogInformation("Program stopped");
    }

    public void StartGateLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _actions.OpenGate();
                Thread.Sleep(5000);

                _actions.OpenGateDuel();
                Thread.Sleep(3000);

                _actions.StartAutoDuel();

                int duelCheckCounter = 0;
                while (!_actions.IsDuelOver())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(10000);
                    if (duelCheckCounter > 4)
                    {
                        bool result = _actions.CheckForNetworkInterruption();

                        if (!result) _actions.ClickScreen();
                    }
                    duelCheckCounter++;

                    if (cancellationToken.IsCancellationRequested) break;
                }

                _logger.LogInformation("Duel Over");

                _actions.ClickPopUpDialogs(_actions.DoesGateExists, cancellationToken);

                Thread.Sleep(2000);

                if (cancellationToken.IsCancellationRequested) break;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Stopping program");
        }

        _logger.LogInformation("Program stopped");
    }
}
