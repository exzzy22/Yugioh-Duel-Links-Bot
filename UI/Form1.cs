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
        _actions.MoveScreenLeft();
    }
}
