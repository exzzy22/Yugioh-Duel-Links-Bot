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
    private CancellationTokenSource _cts = new();

    private readonly Dictionary<string, Tag> _duelists = new Dictionary<string, Tag>
    {
            { "World Duelist", MLDetection.Tag.WorldDuelist },
            { "Legendary Duelist", MLDetection.Tag.LegendaryDuelist },
            { "Vagabond Duelist", MLDetection.Tag.VagabondDuelist }
    };
    private readonly ILogic _logic;
    private readonly ILogger<MainForm> _logger;
    private UserConfiguration _userConfiguration;

    public MainForm(ILogic logic, ILogger<MainForm> logger, UserConfiguration userConfiguration)
    {
        InitializeComponent();
        cycleWorldCheckBox.Checked = true;
        _logic = logic;
        DuelistsListBox.DataSource = new BindingSource(_duelists, null);
        DuelistsListBox.DisplayMember = "Key";
        _logger = logger;
        _userConfiguration = userConfiguration;
        UseGpuCheckBox.Checked = _userConfiguration.UseGpu;
        LaptopCheckBox.Checked = _userConfiguration.IsLaptop;
    }

    private void StartStopButton_Click(object sender, EventArgs e)
    {
        if (GateDuelCheckBox.Checked)
        {
            StartGateDuels();
        }
        else if (RaidDuel.Checked)
        {
            StartRaidEventDuels();
        }
        else if (DuelistRoad.Checked)
        {
            StartDuelistRoadEventDuels();
        }
        else
        {
            StartWorldDuels();
        }
    }

    private void StartWorldDuels()
    {
        StartStopButton.Enabled = false;

        if (StartStopButton.Text.Equals(STOP_TEXT))
        {
            StartStopButton.Text = STOPPING_TEXT;
            _cts.Cancel();
            _cts.Dispose();
            StartStopButton.BackColor = Color.White;
            StartStopButton.Text = START_TEXT;
            _cts = new();
            StartStopButton.Enabled = true;
            _logger.LogInformation("Program stopped");

            return;
        }

        List<Tag> selected = DuelistsListBox.CheckedItems
            .GetValues<string, Tag>()
            .ToList();

        StartStopButton.Text = STOP_TEXT;
        StartStopButton.BackColor = Color.Red;
        StartStopButton.Enabled = true;

        _logic.StartDuelWorldsLoop(_cts.Token, selected, cycleWorldCheckBox.Checked);
    }

    private void StartRaidEventDuels()
    {
        StartStopButton.Enabled = false;

        if (StartStopButton.Text.Equals(STOP_TEXT))
        {
            StartStopButton.Text = STOPPING_TEXT;
            _cts.Cancel();
            _cts.Dispose();
            StartStopButton.BackColor = Color.White;
            StartStopButton.Text = START_TEXT;
            _cts = new();
            StartStopButton.Enabled = true;
            _logger.LogInformation("Program stopped");

            return;
        }

        StartStopButton.Text = STOP_TEXT;
        StartStopButton.BackColor = Color.Red;
        StartStopButton.Enabled = true;

        _logic.StartRaidEventDueldLoop(_cts.Token);
    }

    private void StartDuelistRoadEventDuels()
    {
        StartStopButton.Enabled = false;

        if (StartStopButton.Text.Equals(STOP_TEXT))
        {
            StartStopButton.Text = STOPPING_TEXT;
            _cts.Cancel();
            _cts.Dispose();
            StartStopButton.BackColor = Color.White;
            StartStopButton.Text = START_TEXT;
            _cts = new();
            StartStopButton.Enabled = true;
            _logger.LogInformation("Program stopped");

            return;
        }

        StartStopButton.Text = STOP_TEXT;
        StartStopButton.BackColor = Color.Red;
        StartStopButton.Enabled = true;

        _logic.StartDuelistRoadEventDueldLoop(_cts.Token);
    }

    private void StartGateDuels()
    {
        StartStopButton.Enabled = false;

        if (StartStopButton.Text.Equals(STOP_TEXT))
        {
            StartStopButton.Text = STOPPING_TEXT;
            _cts.Cancel();
            _cts.Dispose();
            StartStopButton.BackColor = Color.White;
            StartStopButton.Text = START_TEXT;
            _cts = new();
            StartStopButton.Enabled = true;
            _logger.LogInformation("Program stopped");

            return;
        }

        List<Tag> selected = DuelistsListBox.CheckedItems
            .GetValues<string, Tag>()
            .ToList();

        StartStopButton.Text = STOP_TEXT;
        StartStopButton.BackColor = Color.Red;
        StartStopButton.Enabled = true;

        _logic.StartGateLoop(_cts.Token);
    }


    private void DuelistsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void UseGpuCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        _userConfiguration.UseGpu = UseGpuCheckBox.Checked;
    }

    private void LaptopCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        _userConfiguration.IsLaptop = LaptopCheckBox.Checked;
    }

    private void RaidDuel_CheckedChanged(object sender, EventArgs e)
    {

    }
}
