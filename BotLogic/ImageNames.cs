namespace BotLogic;

public static class ImageNames
{
    public const string LEFT_ARROW = "LeftArrow.png";
    public const string RIGHT_ARROW = "RightArrow.png";
    public const string DIALOG_NEXT = "Dialog_Next.png";
    public const string AUTO_DUEL = "Auto-Duel.png";
    public const string MATCHOVER_OK = "MatchOver_OK.png";
    public const string MATCHOVER_NEXT = "MatchOver_Next.png";
    public const string MATCHOVER_DIALOG_OK = "Dialog_OK.png";
    public const string MATCHOVER_DIALOG_OK_2 = "Dialog_OK_2.png";
    public const string DUEL_STUDIO = "DuelStudio.png";
    public const string GATE = "Gate.png";
    public const string PVP_ARENA = "PvPArena.png";
    public const string SHOP = "Shop.png";

    public static List<string> MatchOverImages()
    {
        return new List<string>
        {
            MATCHOVER_NEXT,
            MATCHOVER_OK,
            MATCHOVER_DIALOG_OK,
            MATCHOVER_DIALOG_OK_2,
            DIALOG_NEXT,
        };
    }

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