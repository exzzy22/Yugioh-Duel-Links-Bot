using BotLogic.Models;

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
            MATCHOVER_DIALOG_OK_2
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

    public static class Duelists
    {
        public static List<Duelist> GetAllDuelists()
        {
            return new List<Duelist>
        {
            new Duelist { Name = "Ashley", ImagePaths = new List<string>
                {
                    "Duelists\\Ashley_001.png",
                    "Duelists\\Ashley_002.png",
                    "Duelists\\Ashley_003.png",
                    "Duelists\\Ashley_004.png",
                }
            },
            new Duelist { Name = "Bella", ImagePaths = new List<string>
                {
                    "Duelists\\Bella_001.png",
                    "Duelists\\Bella_002.png",
                    "Duelists\\Bella_003.png",
                    "Duelists\\Bella_004.png",
                }
            },
            new Duelist { Name = "Celestina Noodlina", ImagePaths = new List<string>
                {
                    "Duelists\\Celestina_Noodlina_001.png",
                    "Duelists\\Celestina_Noodlina_002.png",
                    "Duelists\\Celestina_Noodlina_003.png",
                    "Duelists\\Celestina_Noodlina_004.png",
                }
            },
            new Duelist { Name = "David", ImagePaths = new List<string> 
                { 
                    "Duelists\\David_001.png",
                    "Duelists\\David_002.png",
                    "Duelists\\David_003.png",
                    "Duelists\\David_004.png",
                }
            },
            new Duelist { Name = "Emma", ImagePaths = new List<string>
                {
                    "Duelists\\Emma_001.png",
                    "Duelists\\Emma_002.png",
                }
            },
            new Duelist { Name = "Gavin Sogetsu", ImagePaths = new List<string>
                { 
                    "Duelists\\Gavin_Sogetsu_001.png",
                    "Duelists\\Gavin_Sogetsu_002.png",
                }
            },
            new Duelist { Name = "Goha Soldier", ImagePaths = new List<string>
                {
                    "Duelists\\Goha_Solider_001.png",
                    "Duelists\\Goha_Solider_002.png",
                    "Duelists\\Goha_Solider_003.png",
                }
            },
            new Duelist { Name = "Jay", ImagePaths = new List<string>
                {
                    "Duelists\\Jay_001.png",
                    "Duelists\\Jay_002.png",
                    "Duelists\\Jay_003.png",
                }
            },
            new Duelist { Name = "Meg", ImagePaths = new List<string>
                {
                    "Duelists\\Meg_001.png",
                    "Duelists\\Meg_002.png",
                    "Duelists\\Meg_003.png",
                }
            },
            new Duelist { Name = "Mickey", ImagePaths = new List<string> 
                { 
                    "Duelists\\Mickey_001.png",
                    "Duelists\\Mickey_002.png",
                }
            },
            new Duelist { Name = "Mimi Imimi", ImagePaths = new List<string>
                {
                    "Duelists\\Mimi_Imimi_001.png",
                }
            },
            new Duelist { Name = "Nick", ImagePaths = new List<string> 
                { 
                    "Duelists\\Nick_001.png",
                    "Duelists\\Nick_002.png",
                }
            },
            new Duelist { Name = "Luke Kallister", ImagePaths = new List<string>
                {
                    "Duelists\\Luke_Kallister_001.png",
                }
            },
            new Duelist { Name = "Saburamen", ImagePaths = new List<string>
                {
                    "Duelists\\Saburamen_001.png",
                    "Duelists\\Saburamen_002.png",
                    "Duelists\\Saburamen_003.png",
                }
            },
            new Duelist { Name = "Vagabond", ImagePaths = new List<string>
                {
                    "Duelists\\Vagabond_001.png",
                    "Duelists\\Vagabond_002.png",
                    "Duelists\\Vagabond_003.png",
                }
            },
            new Duelist { Name = "Yosh Imimi", ImagePaths = new List<string>
                {
                    "Duelists\\Yosh_Imimi_001.png",
                    "Duelists\\Yosh_Imimi_002.png",
                    "Duelists\\Yosh_Imimi_003.png",
                }
            },
        };
        }
    }
}