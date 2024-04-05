using BotLogic.Logic;
using Serilog;

namespace UI;

public partial class MainForm : Form
{
    private readonly ILogic _logic;
    public MainForm(ILogic logic)
    {
        InitializeComponent();
        _logic = logic;
    }

    private async void StartStopButton_Click(object sender, EventArgs e)
    {
        CancellationTokenSource cts = new();

        await _logic.StartDuelWorldLoop(cts.Token);
    }

    private void richTextBox_TextChanged(object sender, EventArgs e)
    {

    }
}
