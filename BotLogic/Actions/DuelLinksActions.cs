using BotLogic.ImageFinder;
using BotLogic.MouseSimulator;
using Microsoft.Extensions.Logging;
using MLDetection;
using MLDetection.Models;
using ScreenCapture.Helpers;
using Point = System.Drawing.Point;

namespace BotLogic.Actions;

public class DuelLinksActions : IActions
{
    private readonly ImageNamesService _imageNamesService;
    private readonly IImageFinder _imageFinder;
    private readonly IMouseSimulator _mouseSimulator;
    private readonly IHelpers _helpers;
    private readonly ILogger<DuelLinksActions> _logger;

    public DuelLinksActions(ImageNamesService imageNamesService, IImageFinder imageFinder, IMouseSimulator mouseSimulator, IHelpers helpers, ILogger<DuelLinksActions> logger)
    {
        _imageNamesService = imageNamesService;
        _imageFinder = imageFinder;
        _mouseSimulator = mouseSimulator;
        _helpers = helpers;
        _logger = logger;
    }

    public bool StartDuel(CancellationToken ct, ObjectPoint objectPoint)
    {
        // Click on duelist
        _mouseSimulator.SimulateMouseClick(objectPoint.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
        Thread.Sleep(3000);

        // Check if auto duel button exists
        Point? autoDuelPoint = _imageFinder.GetImageLocationCV(ct, _imageNamesService.AutoDuel, ProcessNames.DUEL_LINKS);

        if (autoDuelPoint.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(autoDuelPoint.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

            return true;
        }

        // Check if duelist dialogs exists
        List<ObjectPoint> duelistDialogPoint = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.DuelistDialog);

        while (duelistDialogPoint.Count > 0)
        {
            ObjectPoint? dialog = duelistDialogPoint.FirstOrDefault();

            if (dialog is not null)
            {
                _mouseSimulator.SimulateMouseClick(dialog.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

                Thread.Sleep(2000);
            }

            autoDuelPoint = _imageFinder.GetImageLocationCV(ct, _imageNamesService.AutoDuel, ProcessNames.DUEL_LINKS);

            if (autoDuelPoint.HasValue)
            {
                _mouseSimulator.SimulateMouseClick(autoDuelPoint.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

                return true;
            }

            duelistDialogPoint = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.DuelistDialog);
        }

        // Check for missclicks and get back if it was missclick
        List<ObjectPoint> missclickButtons = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tags.MissClickButtons());

        ObjectPoint? backButton = missclickButtons.FirstOrDefault();

        if (backButton is not null)
        {
            _mouseSimulator.SimulateMouseClick(backButton.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(2000);
        }

        return false;
    }

    public void ClickDuelistDialogUntilDissapers(CancellationToken ct)
    {
        _logger.LogInformation(nameof(ClickDuelistDialogUntilDissapers));

        List<ObjectPoint> duelistDialogPoints = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.DuelistDialog);

        while (duelistDialogPoints.Count > 0)
        {
            foreach (ObjectPoint point in duelistDialogPoints)
            {
                _mouseSimulator.SimulateMouseClick(point.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            }
            Thread.Sleep(3000);

            if(_imageFinder.DoesImageExistsCV(ct, _imageNamesService.AutoDuel, ProcessNames.DUEL_LINKS)) { break; }

            duelistDialogPoints = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.DuelistDialog);
        }
    }

    public ObjectPoint OpenDuelistRoadDuel(CancellationToken ct)
    {
        _logger.LogInformation(nameof(OpenDuelistRoadDuel));

        int retryCount = 0;

        Point? startButton = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Start, ProcessNames.DUEL_LINKS);

        if (startButton.HasValue)
        {
            _mouseSimulator.SimulateMouseClick(startButton.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(1000);
            _mouseSimulator.SimulateMouseClick(startButton.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(3000);
        }

        Point? duelbutton = _imageFinder.GetImageLocationCV(ct, _imageNamesService.TurboDuel, ProcessNames.DUEL_LINKS);

        if (!duelbutton.HasValue)
        {
            duelbutton = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Duel, ProcessNames.DUEL_LINKS);
        }
        Thread.Sleep(1000);
        _mouseSimulator.SimulateMouseClick(duelbutton.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

        ClickDuelistDialogUntilLevelSelectAppers(ct);

        Point? diffChose = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Hard, ProcessNames.DUEL_LINKS);

        while (!diffChose.HasValue)
        {
            if (retryCount > 3) break;

            diffChose = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Hard, ProcessNames.DUEL_LINKS);
            retryCount++;
        }

        return new ObjectPoint { Score = 1, Tag = Tag.CVImage, Point = diffChose.Value };
    }

    public ObjectPoint OpenTagDuel(CancellationToken ct)
    {
        _logger.LogInformation(nameof(OpenTagDuel));

        int retryCount = 0;

        Point? duelbutton = _imageFinder.GetImageLocationCV(ct, _imageNamesService.TagDuel, ProcessNames.DUEL_LINKS);

        Thread.Sleep(1000);
        _mouseSimulator.SimulateMouseClick(duelbutton.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

        ClickDuelistDialogUntilLevelSelectAppers(ct);

        Point? diffChose = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Hard, ProcessNames.DUEL_LINKS);

        while (!diffChose.HasValue)
        {
            if (retryCount > 3) break;

            diffChose = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Hard, ProcessNames.DUEL_LINKS);
            retryCount++;
        }

        return new ObjectPoint { Score = 1, Tag = Tag.CVImage, Point = diffChose.Value };
    }

    public void StartAutoDuel(CancellationToken ct)
    { 
        _logger.LogInformation(nameof(StartAutoDuel));

        int retryCount = 0;

        Point? autoDialog = _imageFinder.GetImageLocationCV(ct, _imageNamesService.AutoDuel, ProcessNames.DUEL_LINKS);

        while (!autoDialog.HasValue)
        {
            if (retryCount > 3) return;

            autoDialog = _imageFinder.GetImageLocationCV(ct, _imageNamesService.AutoDuel, ProcessNames.DUEL_LINKS);
            retryCount++;
        }

        _mouseSimulator.SimulateMouseClick(autoDialog.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void OpenGateDuel(CancellationToken ct)
    {
        _logger.LogInformation(nameof(OpenGateDuel));

        int retryCount = 0;

        Point? duel = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Duel, ProcessNames.DUEL_LINKS);

        if (!duel.HasValue)
        {
            duel = _imageFinder.GetImageLocationCV(ct, _imageNamesService.RushDuel, ProcessNames.DUEL_LINKS);
        }

        while (!duel.HasValue)
        {
            if (retryCount > 3) return;

            duel = _imageFinder.GetImageLocationCV(ct, _imageNamesService.Duel, ProcessNames.DUEL_LINKS);
            if (!duel.HasValue)
            {
                duel = _imageFinder.GetImageLocationCV(ct, _imageNamesService.RushDuel, ProcessNames.DUEL_LINKS);
            }
            retryCount++;
        }

        _mouseSimulator.SimulateMouseClick(duel.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

        Thread.Sleep(4000);

        ClickDuelistDialogUntilDissapers(ct);
    }


    public void MoveScreenLeft(CancellationToken ct)
    {
        _logger.LogInformation(nameof(MoveScreenLeft));

        int retryCount = 0;

        Point? point = _imageFinder.GetImageLocationCV(ct, _imageNamesService.LeftArrow, ProcessNames.DUEL_LINKS);

        while(!point.HasValue)
        {
            if (retryCount > 3) return;

            point = _imageFinder.GetImageLocationCV(ct, _imageNamesService.LeftArrow, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public void MoveScreenRight(CancellationToken ct)
    {
        _logger.LogInformation(nameof(MoveScreenRight));

        int retryCount = 0;

        Point? point = _imageFinder.GetImageLocationCV(ct, _imageNamesService.RightArrow, ProcessNames.DUEL_LINKS);

        while (!point.HasValue)
        {
            if (retryCount > 3) return;

            point = _imageFinder.GetImageLocationCV(ct, _imageNamesService.RightArrow, ProcessNames.DUEL_LINKS);
        }

        _mouseSimulator.SimulateMouseClick(point.Value, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public List<ObjectPoint> GetAllWorldDuelistsOnScreen(CancellationToken ct, List<Tag> duelistTypes)
    {
        _logger.LogInformation("Get all world duelists on screen");

        return _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, duelistTypes, 0.7f);
    }

    public void ClickPopUpDialogs(Func<bool> checkHomepage, CancellationToken ct)
    {
        _logger.LogInformation(nameof(ClickPopUpDialogs));

        bool homepageexists = false;
        List<ObjectPoint> clickableButtons;

        while (!homepageexists)
        {
            clickableButtons = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tags.ClickableButtons());

            if (clickableButtons.Count > 0)
            {
                _mouseSimulator.SimulateMouseClick(clickableButtons.First().Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
                Thread.Sleep(2000);
            }
            else
            {
                ClickScreen(ct);
            }

            homepageexists = checkHomepage();
        }

        clickableButtons = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tags.ClickableButtons());

        while (clickableButtons.Count > 0)
        {
            _mouseSimulator.SimulateMouseClick(clickableButtons.First().Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            Thread.Sleep(3000);

            clickableButtons = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tags.ClickableButtons());
            Thread.Sleep(3000);
        }
    }

    public bool IsOnHomepage(CancellationToken ct)
    {
        foreach (var image in _imageNamesService.HomepageImages())
        {
            if (_imageFinder.DoesImageExistsCV(ct, image, ProcessNames.DUEL_LINKS))
            {
                return true;
            }
        }

        _logger.LogInformation("Not on homepage");
        return false;
    }

    public bool DoesGateExists(CancellationToken ct)
    {
        ObjectPoint? gate = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.Gate).FirstOrDefault();

        return gate is not null;
    }

    public bool DoesAssistButtonExists(CancellationToken ct)
    {
        bool result = _imageFinder.DoesImageExistssML(ct,ProcessNames.DUEL_LINKS, Tag.AssistDuelButton);

        if (!result)
        {
            _mouseSimulator.DoMouseScroll(-5000);
        }

        return result;
    }

    public bool DoesStartButtonExists(CancellationToken ct)
    {
        bool result = _imageFinder.DoesImageExistsCV(ct, _imageNamesService.Start, ProcessNames.DUEL_LINKS);

        return result;
    }

    public bool DoesTagDuelButtonExists(CancellationToken ct)
    {
        bool result = _imageFinder.DoesImageExistsCV(ct ,_imageNamesService.TagDuel, ProcessNames.DUEL_LINKS);

        return result;
    }

    public bool IsDuelOver(CancellationToken ct)
    {
        _logger.LogInformation(nameof(IsDuelOver));

        List<ObjectPoint> okButtonPoint = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.OkButton, 0.5f);

        return okButtonPoint.Count > 0;
    }

    public bool DoesTagExists(CancellationToken ct, Tag tag, float score = 0f)
    {
        _logger.LogInformation(nameof(DoesTagExists));

        List<ObjectPoint> objects = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, tag, score);

        return objects.Count > 0;
    }

    public void ClickScreen(CancellationToken ct)
    {
        _logger.LogInformation(nameof(ClickScreen));

        Point point = _imageFinder.GetImagePosition(ct, ProcessNames.DUEL_LINKS, ImageAlignment.Bottom);

        _mouseSimulator.SimulateMouseClick(point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    public bool CheckForNetworkInterruption(CancellationToken ct)
    {
        List<ObjectPoint> retryPoint = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.RetryButton, 0.7f);

        if (retryPoint.Count > 0)
        {
            _logger.LogInformation("Click network interruption error popup, score: {Score} and Tag {Tag}", retryPoint.First().Score, retryPoint.First().Tag);
            _mouseSimulator.SimulateMouseClick(retryPoint.First().Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

            Thread.Sleep(5000);

            return true;
        }

        return false;
    }

    public bool ChangeWorld(CancellationToken ct,Tag world)
    {
        List<ObjectPoint> worldMenu = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.WorldMenu, 0.5f);

        if (worldMenu.Count < 1)
        {
            _logger.LogError("No world menu button");
            return false;
        }

        _mouseSimulator.SimulateMouseClick(worldMenu.First().Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

        Thread.Sleep(2000);

        List<ObjectPoint> worldToGo = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, world, 0.35f);

        if (worldToGo.Count < 1)
        {
            _logger.LogError("No world to go");
            return false;
        }

        _mouseSimulator.SimulateMouseClick(worldToGo.First().Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));

        Thread.Sleep(9000);

        return true;
    }

    public void OpenGate(CancellationToken ct)
    {
        _logger.LogInformation(nameof(OpenGate));

        int retryCount = 0;

        ObjectPoint? gate = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.Gate).FirstOrDefault();

        while (gate is null)
        {
            if (retryCount > 3) return;

            gate = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.Gate).FirstOrDefault();
            retryCount++;
        }

        _mouseSimulator.SimulateMouseClick(gate.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
    }

    private void ClickDuelistDialogUntilLevelSelectAppers(CancellationToken ct)
    {
        _logger.LogInformation(nameof(ClickDuelistDialogUntilLevelSelectAppers));

        List<ObjectPoint> duelistDialogPoints = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.DuelistDialog);

        while (!_imageFinder.DoesImageExistsCV(ct, _imageNamesService.Hard, ProcessNames.DUEL_LINKS))
        {
            foreach (ObjectPoint point in duelistDialogPoints)
            {
                _mouseSimulator.SimulateMouseClick(point.Point, _helpers.GetWindowHandle(ProcessNames.DUEL_LINKS));
            }
            Thread.Sleep(2000);

            duelistDialogPoints = _imageFinder.GetImagesLocationsML(ct, ProcessNames.DUEL_LINKS, Tag.DuelistDialog);
        }
    }
}
