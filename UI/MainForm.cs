using BotLogic.Logic;
using MLDetection;
using UI.Extensions;

namespace UI;

public partial class MainForm : Form
{
    private const string START_TEXT = "Start";
    private const string STOP_TEXT = "Stop";
    private CancellationTokenSource _cts = new ();

    private readonly Dictionary<string, string> _duelists = new Dictionary<string, string> 
    {
            { "World Duelist", Tags.WORLD_DUELIST },
            { "Legendary Duelist", Tags.LEGENDARY_DUELIST },
            { "Vagabond Duelist", Tags.VAGABOND_DUELIST }
    }; 
    private readonly ILogic _logic;
    public MainForm(ILogic logic)
    {
        InitializeComponent();
        _logic = logic;
        DuelistsListBox.DataSource = new BindingSource(_duelists, null);
        DuelistsListBox.DisplayMember = "Key";
    }

    private async void StartStopButton_Click(object sender, EventArgs e)
    {
        if (StartStopButton.Text.Equals(STOP_TEXT))
        {
            await _cts.CancelAsync();
            _cts.Dispose();
            _cts = new ();
            StartStopButton.Text = START_TEXT;
            StartStopButton.BackColor = Color.White;

            return;
        }

        List<string> selected = DuelistsListBox.CheckedItems
            .GetValues<string,string>()
            .ToList();

        StartStopButton.Text = STOP_TEXT;
        StartStopButton.BackColor = Color.Red;

        await _logic.StartDuelWorldLoop(_cts.Token, selected);
    }

    private void DuelistsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
