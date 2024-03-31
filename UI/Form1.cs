using BotLogic.Actions;

namespace UI;

public partial class MainForm: Form
{
    private readonly IActions _actions;
    public MainForm(IActions actions)
    {
        InitializeComponent();
        _actions = actions;
    }

    private void StartStopButton_Click(object sender, EventArgs e)
    {
        var duelists = _actions.GetAllAvalivableDuelistsOnScreen();

        foreach (var duelist in duelists)
        {
            _actions.ClickDuelist(duelist);
            //_actions.ClickDuelistDialogUntilDissapers();
            //_actions.StartAutoDuel();
        }
    }
}
