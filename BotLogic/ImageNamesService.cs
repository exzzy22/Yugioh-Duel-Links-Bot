using MLDetection;

namespace BotLogic;

public class ImageNamesService
{
    private readonly UserConfiguration _configuration;

    public ImageNamesService(UserConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string LeftArrow => GetImageName(nameof(LeftArrow));
    public string RightArrow => GetImageName(nameof(RightArrow));
    public string AutoDuel => GetImageName(nameof(AutoDuel));
    public string DuelStudio => GetImageName(nameof(DuelStudio));
    public string Gate => GetImageName(nameof(Gate));
    public string PvPArena => GetImageName(nameof(PvPArena));
    public string Shop => GetImageName(nameof(Shop));
    public string Duel => GetImageName(nameof(Duel));

    public List<string> HomepageImages()
    {
        return new List<string>
        {
            DuelStudio,
            Gate,
            PvPArena,
            Shop
        };
    }

    private string GetImageName(string operation)
    {
        bool isLaptop = _configuration.IsLaptop;

        switch (operation)
        {
            case nameof(LeftArrow):
                return isLaptop ? "LeftArrow_Lap.png" : "LeftArrow.png";
            case nameof(RightArrow):
                return isLaptop ? "RightArrow_Lap.png" : "RightArrow.png";
            case nameof(AutoDuel):
                return isLaptop ? "Auto-Duel_Lap.png" : "Auto-Duel.png";
            case nameof(DuelStudio):
                return isLaptop ? "DuelStudio_Lap.png" : "DuelStudio.png";
            case nameof(Gate):
                return isLaptop ? "Gate_Lap.png" : "Gate.png";
            case nameof(PvPArena):
                return isLaptop ? "PvPArena_Lap.png" : "PvPArena.png";
            case nameof(Shop):
                return isLaptop ? "Shop_Lap.png" : "Shop.png";
            case nameof(Duel):
                return isLaptop ? "Duel_Lap.png" : "Duel.png";
            default:
                // Handle unknown operation (return a default image name)
                return "UnknownImage.png";
        }
    }
}
