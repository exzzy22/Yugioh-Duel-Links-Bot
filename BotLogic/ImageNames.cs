namespace BotLogic;

public static class ImageNames
{
    public const string LEFT_ARROW = "LeftArrow.png";
    public const string RIGHT_ARROW = "RightArrow.png";
    public const string AUTO_DUEL = "Auto-Duel.png";
    public const string DUEL_STUDIO = "DuelStudio.png";
    public const string GATE = "Gate.png";
    public const string PVP_ARENA = "PvPArena.png";
    public const string SHOP = "Shop.png";

    public static List<string> HomepageImages()
    {
        return new List<string>
        {
            DUEL_STUDIO,
            GATE,
            PVP_ARENA,
            SHOP
        };
    }
}