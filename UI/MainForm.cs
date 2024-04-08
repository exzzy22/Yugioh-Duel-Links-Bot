using BotLogic.Logic;
using Microsoft.Extensions.Logging;
using MLDetection;
using UI.Extensions;

namespace UI;

public partial class MainForm : Form
{
    private const string START_TEXT = "Start";
    private const string STOP_TEXT = "Stop";
    private const string STOPPING_TEXT = "Stopping";
    private CancellationTokenSource _cts = new ();
    private Task? _task;

    private readonly Dictionary<string, string> _duelists = new Dictionary<string, string> 
    {
            { "World Duelist", Tags.WORLD_DUELIST },
            { "Legendary Duelist", Tags.LEGENDARY_DUELIST },
            { "Vagabond Duelist", Tags.VAGABOND_DUELIST }
    }; 
    private readonly ILogic _logic;
    private readonly ILogger<MainForm> _logger;

    public MainForm(ILogic logic, ILogger<MainForm> logger)
    {
        InitializeComponent();
        _logic = logic;
        DuelistsListBox.DataSource = new BindingSource(_duelists, null);
        DuelistsListBox.DisplayMember = "Key";
        _logger = logger;
    }

    private async void StartStopButton_Click(object sender, EventArgs e)
    {
        StartStopButton.Enabled = false;

        if (StartStopButton.Text.Equals(STOP_TEXT))
        {
            StartStopButton.Text = STOPPING_TEXT;
            await _cts.CancelAsync();
            _cts.Dispose();
            StartStopButton.BackColor = Color.White;
            StartStopButton.Text = START_TEXT;
            _cts = new();
            StartStopButton.Enabled = true;
            _task?.Dispose();
            _logger.LogInformation("Program stopped");

            return;
        }

        List<string> selected = DuelistsListBox.CheckedItems
            .GetValues<string,string>()
            .ToList();

        StartStopButton.Text = STOP_TEXT;
        StartStopButton.BackColor = Color.Red;
        StartStopButton.Enabled = true;

        _task = Task.Run(async () => { await _logic.StartNetworkInterruptionChecker(_cts.Token); });

        await _logic.StartDuelWorldLoop(_cts.Token, selected);
    }

    private void DuelistsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
