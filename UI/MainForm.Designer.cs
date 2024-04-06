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
            SuspendLayout();
            // 
            // StartStopButton
            // 
            StartStopButton.Location = new Point(35, 34);
            StartStopButton.Name = "StartStopButton";
            StartStopButton.Size = new Size(75, 23);
            StartStopButton.TabIndex = 0;
            StartStopButton.Text = "Start";
            StartStopButton.UseVisualStyleBackColor = true;
            StartStopButton.Click += StartStopButton_Click;
            // 
            // richTextBoxLogControl
            // 
            richTextBoxLogControl.ForContext = "";
            richTextBoxLogControl.Location = new Point(12, 177);
            richTextBoxLogControl.Name = "richTextBoxLogControl";
            richTextBoxLogControl.ReadOnly = true;
            richTextBoxLogControl.Size = new Size(1144, 594);
            richTextBoxLogControl.TabIndex = 1;
            richTextBoxLogControl.Text = "";
            // 
            // DuelistsListBox
            // 
            DuelistsListBox.FormattingEnabled = true;
            DuelistsListBox.Location = new Point(585, 34);
            DuelistsListBox.Name = "DuelistsListBox";
            DuelistsListBox.Size = new Size(241, 94);
            DuelistsListBox.TabIndex = 2;
            DuelistsListBox.SelectedIndexChanged += DuelistsListBox_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1168, 783);
            Controls.Add(DuelistsListBox);
            Controls.Add(richTextBoxLogControl);
            Controls.Add(StartStopButton);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button StartStopButton;
        private Serilog.Sinks.WinForms.Core.RichTextBoxLogControl richTextBoxLogControl;
        private CheckedListBox DuelistsListBox;
    }
}
