using BotLogic.Logic;

namespace UI;

public partial class MainForm: Form
{
    private readonly ILogic _logic;
    public MainForm(ILogic logic)
    {
        InitializeComponent();
        _logic = logic;
    }

    private void StartStopButton_Click(object sender, EventArgs e)
    {
        CancellationTokenSource cts = new ();

        _logic.StartDuelWorldLoop(cts.Token);
    }
}
