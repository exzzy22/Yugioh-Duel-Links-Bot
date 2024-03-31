namespace BotLogic;

public static class ImageNames
{
    public const string LEFT_ARROW = "LeftArrow.png";
    public const string RIGHT_ARROW = "RightArrow.png";
    public const string DIALOG_NEXT = "Dialog_Next.png";
    public const string AUTO_DUEL = "Auto-Duel.png";

    public static class Duelists
    {
        public const string ASHLEY = "Duelists\\Ashley.png";
        public const string BELLA = "Duelists\\Bella.png";
        public const string CELESTINA_NOODLINA = "Duelists\\Celestina_Noodlina.png";
        public const string DAVID = "Duelists\\David.png";
        public const string GAVIN_SOGETSU = "Duelists\\Gavin_Sogetsu.png";
        public const string GOHA_SOLDIER = "Duelists\\Goha_Solider.png";
        public const string JAY = "Duelists\\Jay.png";
        public const string MICKEY = "Duelists\\Mickey.png";
        public const string NICK = "Duelists\\Nick.png";
        public const string SABURAMEN = "Duelists\\Saburamen.png";
        public const string VAGABOND = "Duelists\\Vagabond.png";
        public const string YOSH_IMIMI = "Duelists\\Yosh_Imimi.png";

        public static List<string> GetAllDuelists()
        {
            return new ()
            {
                ASHLEY,
                BELLA,
                CELESTINA_NOODLINA,
                DAVID,
                GAVIN_SOGETSU,
                GOHA_SOLDIER,
                JAY,
                MICKEY,
                NICK,
                SABURAMEN,
                VAGABOND,
                YOSH_IMIMI
            };
        }
    }
}