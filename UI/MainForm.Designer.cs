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
            EventCheckBox = new CheckBox();
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
            // EventCheckBox
            // 
            EventCheckBox.AutoSize = true;
            EventCheckBox.Location = new Point(323, 46);
            EventCheckBox.Name = "EventCheckBox";
            EventCheckBox.Size = new Size(108, 19);
            EventCheckBox.TabIndex = 3;
            EventCheckBox.Text = "EventCheckBox";
            EventCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 611);
            Controls.Add(EventCheckBox);
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
        private CheckBox EventCheckBox;
    }
}
