namespace UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StartStopButton = new Button();
            richTextBoxLogControl = new Serilog.Sinks.WinForms.Core.RichTextBoxLogControl();
            DuelistsListBox = new CheckedListBox();
            RaidDuel = new CheckBox();
            cycleWorldCheckBox = new CheckBox();
            GateDuelCheckBox = new CheckBox();
            UseGpuCheckBox = new CheckBox();
            LaptopCheckBox = new CheckBox();
            DuelistRoad = new CheckBox();
            tagDuelCheckbox = new CheckBox();
            SuspendLayout();
            // 
            // StartStopButton
            // 
            StartStopButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StartStopButton.Location = new Point(35, 34);
            StartStopButton.Name = "StartStopButton";
            StartStopButton.Size = new Size(110, 47);
            StartStopButton.TabIndex = 0;
            StartStopButton.Text = "Start";
            StartStopButton.UseVisualStyleBackColor = true;
            StartStopButton.Click += StartStopButton_Click;
            // 
            // richTextBoxLogControl
            // 
            richTextBoxLogControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBoxLogControl.ForContext = "";
            richTextBoxLogControl.Location = new Point(12, 202);
            richTextBoxLogControl.Name = "richTextBoxLogControl";
            richTextBoxLogControl.ReadOnly = true;
            richTextBoxLogControl.Size = new Size(788, 397);
            richTextBoxLogControl.TabIndex = 1;
            richTextBoxLogControl.Text = "";
            // 
            // DuelistsListBox
            // 
            DuelistsListBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DuelistsListBox.FormattingEnabled = true;
            DuelistsListBox.Location = new Point(554, 34);
            DuelistsListBox.Name = "DuelistsListBox";
            DuelistsListBox.Size = new Size(246, 148);
            DuelistsListBox.TabIndex = 2;
            DuelistsListBox.SelectedIndexChanged += DuelistsListBox_SelectedIndexChanged;
            // 
            // RaidDuel
            // 
            RaidDuel.AutoSize = true;
            RaidDuel.Location = new Point(323, 46);
            RaidDuel.Name = "RaidDuel";
            RaidDuel.Size = new Size(76, 19);
            RaidDuel.TabIndex = 3;
            RaidDuel.Text = "Raid Duel";
            RaidDuel.UseVisualStyleBackColor = true;
            RaidDuel.CheckedChanged += RaidDuel_CheckedChanged;
            // 
            // cycleWorldCheckBox
            // 
            cycleWorldCheckBox.AutoSize = true;
            cycleWorldCheckBox.Location = new Point(177, 71);
            cycleWorldCheckBox.Name = "cycleWorldCheckBox";
            cycleWorldCheckBox.Size = new Size(95, 19);
            cycleWorldCheckBox.TabIndex = 4;
            cycleWorldCheckBox.Text = "Cycle Worlds";
            cycleWorldCheckBox.UseVisualStyleBackColor = true;
            // 
            // GateDuelCheckBox
            // 
            GateDuelCheckBox.AutoSize = true;
            GateDuelCheckBox.Location = new Point(324, 21);
            GateDuelCheckBox.Name = "GateDuelCheckBox";
            GateDuelCheckBox.Size = new Size(74, 19);
            GateDuelCheckBox.TabIndex = 5;
            GateDuelCheckBox.Text = "GateDuel";
            GateDuelCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseGpuCheckBox
            // 
            UseGpuCheckBox.AutoSize = true;
            UseGpuCheckBox.Location = new Point(177, 22);
            UseGpuCheckBox.Name = "UseGpuCheckBox";
            UseGpuCheckBox.Size = new Size(70, 19);
            UseGpuCheckBox.TabIndex = 6;
            UseGpuCheckBox.Text = "Use Gpu";
            UseGpuCheckBox.UseVisualStyleBackColor = true;
            UseGpuCheckBox.CheckedChanged += UseGpuCheckBox_CheckedChanged;
            // 
            // LaptopCheckBox
            // 
            LaptopCheckBox.AutoSize = true;
            LaptopCheckBox.Location = new Point(177, 46);
            LaptopCheckBox.Name = "LaptopCheckBox";
            LaptopCheckBox.Size = new Size(126, 19);
            LaptopCheckBox.TabIndex = 7;
            LaptopCheckBox.Text = "Use Laptop images";
            LaptopCheckBox.UseVisualStyleBackColor = true;
            LaptopCheckBox.CheckedChanged += LaptopCheckBox_CheckedChanged;
            // 
            // DuelistRoad
            // 
            DuelistRoad.AutoSize = true;
            DuelistRoad.Location = new Point(323, 71);
            DuelistRoad.Name = "DuelistRoad";
            DuelistRoad.Size = new Size(92, 19);
            DuelistRoad.TabIndex = 8;
            DuelistRoad.Text = "Duelist Road";
            DuelistRoad.UseVisualStyleBackColor = true;
            // 
            // tagDuelCheckbox
            // 
            tagDuelCheckbox.AutoSize = true;
            tagDuelCheckbox.Location = new Point(323, 98);
            tagDuelCheckbox.Name = "tagDuelCheckbox";
            tagDuelCheckbox.Size = new Size(71, 19);
            tagDuelCheckbox.TabIndex = 9;
            tagDuelCheckbox.Text = "Tag Duel";
            tagDuelCheckbox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 611);
            Controls.Add(tagDuelCheckbox);
            Controls.Add(DuelistRoad);
            Controls.Add(LaptopCheckBox);
            Controls.Add(UseGpuCheckBox);
            Controls.Add(GateDuelCheckBox);
            Controls.Add(cycleWorldCheckBox);
            Controls.Add(RaidDuel);
            Controls.Add(DuelistsListBox);
            Controls.Add(richTextBoxLogControl);
            Controls.Add(StartStopButton);
            Name = "MainForm";
            Text = "Yu-Gi-Oh! Duel Links Bot";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartStopButton;
        private Serilog.Sinks.WinForms.Core.RichTextBoxLogControl richTextBoxLogControl;
        private CheckedListBox DuelistsListBox;
        private CheckBox RaidDuel;
        private CheckBox cycleWorldCheckBox;
        private CheckBox GateDuelCheckBox;
        private CheckBox UseGpuCheckBox;
        private CheckBox LaptopCheckBox;
        private CheckBox DuelistRoad;
        private CheckBox tagDuelCheckbox;
    }
}
