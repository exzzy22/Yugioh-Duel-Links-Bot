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
    private readonly ImageNamesService _imageNamesService;

    public Logic(IActions actions, IImageFinder imageFinder, IMouseSimulator mouseSimulatorq, ILogger<Logic> logger, ImageNamesService imageNamesService)
    {
        _actions = actions;
        _imageFinder = imageFinder;
        _mouseSimulator = mouseSimulatorq;
        _logger = logger;
        _imageNamesService = imageNamesService;
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

                bool isChanged = _actions.ChangeWorld(cancellationToken, currentWorld);

                if (!isChanged)
                {
                    _logger.LogError("World change failed");
                    bool result = _actions.CheckForNetworkInterruption(cancellationToken);
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

                List<ObjectPoint> duelistsOnHomepage = _actions.GetAllWorldDuelistsOnScreen(cancellationToken, duelistTypes);

                _logger.LogInformation("Found {Count} Duelists", duelistsOnHomepage.Count);

                cancellationToken.ThrowIfCancellationRequested();

                if (duelistsOnHomepage.Count < 1)
                {
                    _actions.CheckForNetworkInterruption(cancellationToken);
                }

                foreach (var point in duelistsOnHomepage)
                {
                    bool duelistExists = _actions.StartDuel(cancellationToken, point);
                    _logger.LogInformation("Click {Tag}", point.Tag);

                    if (!duelistExists) continue;

                    int duelCheckCounter = 0;
                    while (!_actions.IsDuelOver(cancellationToken))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        Thread.Sleep(10000);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption(cancellationToken);

                            if (!result) _actions.ClickScreen(cancellationToken);
                        }
                        duelCheckCounter++;

                        if (cancellationToken.IsCancellationRequested) break;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs(() => _actions.IsOnHomepage(cancellationToken), cancellationToken);
                }

                cancellationToken.ThrowIfCancellationRequested();
                _actions.MoveScreenRight(cancellationToken);
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

    public void StartRaidEventDueldLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                List<ObjectPoint> assistDuelPoint = _imageFinder.GetImagesLocationsML(cancellationToken, ProcessNames.DUEL_LINKS, Tag.AssistDuelButton);

                _logger.LogInformation("Found {Count} AssistedDuelButtons", assistDuelPoint.Count);

                if (assistDuelPoint.Count < 1)
                {
                    bool isNetworkInterUpted = _actions.CheckForNetworkInterruption(cancellationToken);

                    if (!isNetworkInterUpted)
                    {
                        _actions.ClickScreen(cancellationToken);
                        Thread.Sleep(2000);
                        _actions.ClickPopUpDialogs(() => _actions.DoesAssistButtonExists(cancellationToken), cancellationToken);
                    }

                    if(cancellationToken.IsCancellationRequested) break;
                }
                else
                {
                    ObjectPoint buttonPoint = assistDuelPoint.First();
                    bool buttonPressed = _actions.StartDuel(cancellationToken, buttonPoint);

                    int duelCheckCounter = 0;
                    while (!_actions.IsDuelOver(cancellationToken))
                    {
                        Thread.Sleep(10000);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption(cancellationToken);

                            if (!result) _actions.ClickScreen(cancellationToken);
                        }
                        duelCheckCounter++;

                        if (cancellationToken.IsCancellationRequested) break;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs(() => _actions.DoesAssistButtonExists(cancellationToken), cancellationToken);

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

    public void StartTagDuelLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var tagDuelButton = _imageFinder.GetImageLocationCV(cancellationToken, _imageNamesService.TagDuel, ProcessNames.DUEL_LINKS);

                if (!tagDuelButton.HasValue)
                {
                    _logger.LogInformation("Cant find tag duel button");

                    bool isNetworkInterUpted = _actions.CheckForNetworkInterruption(cancellationToken);

                    if (!isNetworkInterUpted)
                    {
                        _actions.ClickScreen(cancellationToken);
                        Thread.Sleep(2000);
                        _actions.ClickPopUpDialogs(() => _actions.DoesTagDuelButtonExists(cancellationToken), cancellationToken);
                    }

                    if (cancellationToken.IsCancellationRequested) break;
                }
                else
                {
                    var diffbutton = _actions.OpenTagDuel(cancellationToken);

                    bool buttonPressed = _actions.StartDuel(cancellationToken, diffbutton);

                    int duelCheckCounter = 0;
                    while (!_actions.IsDuelOver(cancellationToken))
                    {
                        Thread.Sleep(10000);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption(cancellationToken);

                            if (!result) _actions.ClickScreen(cancellationToken);
                        }
                        duelCheckCounter++;

                        if (cancellationToken.IsCancellationRequested) break;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs(() => _actions.DoesTagDuelButtonExists(cancellationToken), cancellationToken);

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
                _actions.OpenGate(cancellationToken);
                Thread.Sleep(5000);

                _actions.OpenGateDuel(cancellationToken);
                Thread.Sleep(3000);

                _actions.StartAutoDuel(cancellationToken);

                int duelCheckCounter = 0;
                while (!_actions.IsDuelOver(cancellationToken))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(10000);
                    if (duelCheckCounter > 4)
                    {
                        bool result = _actions.CheckForNetworkInterruption(cancellationToken);

                        if (!result) _actions.ClickScreen(cancellationToken);
                    }
                    duelCheckCounter++;

                    if (cancellationToken.IsCancellationRequested) break;
                }

                _logger.LogInformation("Duel Over");

                _actions.ClickPopUpDialogs(() => _actions.DoesGateExists(cancellationToken), cancellationToken);

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

    public void StartDuelistRoadEventDueldLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var startButton = _imageFinder.GetImageLocationCV(cancellationToken, _imageNamesService.Start, ProcessNames.DUEL_LINKS);
                var turboDuelButton = _imageFinder.GetImageLocationCV(cancellationToken,_imageNamesService.TurboDuel, ProcessNames.DUEL_LINKS);
                var duelButton = _imageFinder.GetImageLocationCV(cancellationToken, _imageNamesService.Duel, ProcessNames.DUEL_LINKS);


                if (!startButton.HasValue && !turboDuelButton.HasValue && !duelButton.HasValue)
                {
                    _logger.LogInformation("Cant find start button");

                    bool isNetworkInterUpted = _actions.CheckForNetworkInterruption(cancellationToken);

                    if (!isNetworkInterUpted)
                    {
                        _actions.ClickScreen(cancellationToken);
                        Thread.Sleep(2000);
                        _actions.ClickPopUpDialogs(() => _actions.DoesStartButtonExists(cancellationToken), cancellationToken);
                    }

                    if (cancellationToken.IsCancellationRequested) break;
                }
                else
                {
                    var diffbutton = _actions.OpenDuelistRoadDuel(cancellationToken);

                    bool buttonPressed = _actions.StartDuel(cancellationToken, diffbutton);

                    int duelCheckCounter = 0;
                    while (!_actions.IsDuelOver(cancellationToken))
                    {
                        Thread.Sleep(10000);
                        if (duelCheckCounter > 4)
                        {
                            bool result = _actions.CheckForNetworkInterruption(cancellationToken);

                            if (!result) _actions.ClickScreen(cancellationToken);
                        }
                        duelCheckCounter++;

                        if (cancellationToken.IsCancellationRequested) break;
                    }

                    _logger.LogInformation("Duel Over");

                    _actions.ClickPopUpDialogs(() => _actions.DoesStartButtonExists(cancellationToken), cancellationToken);

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
}
