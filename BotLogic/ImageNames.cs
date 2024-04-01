namespace BotLogic;

public static class ImageNames
{
    public const string LEFT_ARROW = "LeftArrow.png";
    public const string RIGHT_ARROW = "RightArrow.png";
    public const string DIALOG_NEXT = "Dialog_Next.png";
    public const string AUTO_DUEL = "Auto-Duel.png";

    public static class Duelists
    {
        public static List<Duelist> GetAllDuelists()
        {
            return new List<Duelist>
        {
            new Duelist { Name = "Ashley", ImagePaths = new List<string>
                {
                    "Duelists\\Ashley_001.png",
                    "Duelists\\Ashley_002.png"
                }
            },
            new Duelist { Name = "Bella", ImagePaths = new List<string>
                {
                    "Duelists\\Bella_001.png",
                    "Duelists\\Bella_002.png"
                }
            },
            new Duelist { Name = "Celestina Noodlina", ImagePaths = new List<string>
                {
                    "Duelists\\Celestina_Noodlina_001.png",
                    "Duelists\\Celestina_Noodlina_002.png"
                }
            },
            new Duelist { Name = "David", ImagePaths = new List<string> 
                { 
                "Duelists\\David_001.png",
                }
            },
            new Duelist { Name = "Gavin Sogetsu", ImagePaths = new List<string>
                { 
                "Duelists\\Gavin_Sogetsu_001.png"
                }
            },
            new Duelist { Name = "Goha Soldier", ImagePaths = new List<string>
                {
                    "Duelists\\Goha_Solider_001.png",
                    "Duelists\\Goha_Solider_002.png"
                }
            },
            new Duelist { Name = "Jay", ImagePaths = new List<string>
                {
                    "Duelists\\Jay_001.png",
                    "Duelists\\Jay_002.png"
                }
            },
            new Duelist { Name = "Mickey", ImagePaths = new List<string> 
                { 
                    "Duelists\\Mickey_001.png"
                }
            },
            new Duelist { Name = "Nick", ImagePaths = new List<string> 
                { 
                    "Duelists\\Nick_001.png"
                }
            },
            new Duelist { Name = "Saburamen", ImagePaths = new List<string>
                {
                    "Duelists\\Saburamen_001.png",
                    "Duelists\\Saburamen_002.png"
                }
            },
            new Duelist { Name = "Vagabond", ImagePaths = new List<string>
                {
                    "Duelists\\Vagabond_001.png",
                    "Duelists\\Vagabond_002.png"
                }
            },
            new Duelist { Name = "Yosh Imimi", ImagePaths = new List<string>
                {
                    "Duelists\\Yosh_Imimi_001.png",
                    "Duelists\\Yosh_Imimi_002.png"
                }
            },
            new Duelist { Name = "Emma", ImagePaths = new List<string>
                {
                    "Duelists\\Emma_001.png",
                    "Duelists\\Emma_002.png"
                }
            },
        };
        }
    }
}

public class Duelist
{
    public string Name { get; set; }
    public List<string> ImagePaths { get; set; } = [];
}