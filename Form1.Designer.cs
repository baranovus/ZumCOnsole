namespace ZumConsole
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            if (logfile != null)
            {
                logfile.Close();
            }
            if (client != null)
            {
                client.Close();
            }
            if (scriptfile != null)
            {
                scriptfile.Close();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.FileOpen = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.ConsOutput = new System.Windows.Forms.RichTextBox();
            this.DiagLabel = new System.Windows.Forms.Label();
            this.Log_button = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ConnSettings = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SensConsCommand = new System.Windows.Forms.Button();
            this.ConsInputLabel = new System.Windows.Forms.Label();
            this.HostNameLabel = new System.Windows.Forms.Label();
            this.AppendCRLF = new System.Windows.Forms.CheckBox();
            this.CRLFlabel = new System.Windows.Forms.Label();
            this.ClearScreen = new System.Windows.Forms.Button();
            this.StopScriptButton = new System.Windows.Forms.Button();
            this.ascii = new System.Windows.Forms.RadioButton();
            this.hex = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FileOpen
            // 
            this.FileOpen.Location = new System.Drawing.Point(366, 12);
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.Size = new System.Drawing.Size(88, 31);
            this.FileOpen.TabIndex = 0;
            this.FileOpen.Text = "Script file";
            this.FileOpen.UseVisualStyleBackColor = true;
            this.FileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(460, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(73, 31);
            this.Start.TabIndex = 5;
            this.Start.Text = "Run script";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(213, 12);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(68, 31);
            this.Stop.TabIndex = 6;
            this.Stop.Text = "Log Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // ConsOutput
            // 
            this.ConsOutput.AcceptsTab = true;
            this.ConsOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConsOutput.Location = new System.Drawing.Point(12, 64);
            this.ConsOutput.Name = "ConsOutput";
            this.ConsOutput.Size = new System.Drawing.Size(685, 456);
            this.ConsOutput.TabIndex = 7;
            this.ConsOutput.Text = "";
            // 
            // DiagLabel
            // 
            this.DiagLabel.AutoSize = true;
            this.DiagLabel.Location = new System.Drawing.Point(50, 586);
            this.DiagLabel.Name = "DiagLabel";
            this.DiagLabel.Size = new System.Drawing.Size(0, 13);
            this.DiagLabel.TabIndex = 13;
            // 
            // Log_button
            // 
            this.Log_button.Location = new System.Drawing.Point(120, 12);
            this.Log_button.Name = "Log_button";
            this.Log_button.Size = new System.Drawing.Size(87, 31);
            this.Log_button.TabIndex = 14;
            this.Log_button.Text = "Log file";
            this.Log_button.UseVisualStyleBackColor = true;
            this.Log_button.Click += new System.EventHandler(this.Log_button_Click);
            // 
            // ConnSettings
            // 
            this.ConnSettings.Location = new System.Drawing.Point(12, 12);
            this.ConnSettings.Name = "ConnSettings";
            this.ConnSettings.Size = new System.Drawing.Size(102, 31);
            this.ConnSettings.TabIndex = 15;
            this.ConnSettings.Text = "Comm Settings";
            this.ConnSettings.UseVisualStyleBackColor = true;
            this.ConnSettings.Click += new System.EventHandler(this.ConnSettings_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(53, 551);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(497, 20);
            this.textBox1.TabIndex = 16;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SensConsCommand
            // 
            this.SensConsCommand.Location = new System.Drawing.Point(608, 544);
            this.SensConsCommand.Name = "SensConsCommand";
            this.SensConsCommand.Size = new System.Drawing.Size(73, 31);
            this.SensConsCommand.TabIndex = 17;
            this.SensConsCommand.Text = "Send";
            this.SensConsCommand.UseVisualStyleBackColor = true;
            this.SensConsCommand.Click += new System.EventHandler(this.SensConsCommand_Click);
            // 
            // ConsInputLabel
            // 
            this.ConsInputLabel.AutoSize = true;
            this.ConsInputLabel.Location = new System.Drawing.Point(50, 535);
            this.ConsInputLabel.Name = "ConsInputLabel";
            this.ConsInputLabel.Size = new System.Drawing.Size(31, 13);
            this.ConsInputLabel.TabIndex = 18;
            this.ConsInputLabel.Text = "Input";
            // 
            // HostNameLabel
            // 
            this.HostNameLabel.AutoSize = true;
            this.HostNameLabel.Location = new System.Drawing.Point(20, 587);
            this.HostNameLabel.Name = "HostNameLabel";
            this.HostNameLabel.Size = new System.Drawing.Size(0, 13);
            this.HostNameLabel.TabIndex = 19;
            // 
            // AppendCRLF
            // 
            this.AppendCRLF.AutoSize = true;
            this.AppendCRLF.Checked = true;
            this.AppendCRLF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AppendCRLF.Location = new System.Drawing.Point(579, 557);
            this.AppendCRLF.Name = "AppendCRLF";
            this.AppendCRLF.Size = new System.Drawing.Size(15, 14);
            this.AppendCRLF.TabIndex = 20;
            this.AppendCRLF.UseVisualStyleBackColor = true;
            this.AppendCRLF.CheckedChanged += new System.EventHandler(this.AppendCRLF_CheckedChanged);
            // 
            // CRLFlabel
            // 
            this.CRLFlabel.AutoSize = true;
            this.CRLFlabel.Location = new System.Drawing.Point(559, 535);
            this.CRLFlabel.Name = "CRLFlabel";
            this.CRLFlabel.Size = new System.Drawing.Size(43, 13);
            this.CRLFlabel.TabIndex = 21;
            this.CRLFlabel.Text = "+ CRLF";
            // 
            // ClearScreen
            // 
            this.ClearScreen.Location = new System.Drawing.Point(687, 544);
            this.ClearScreen.Name = "ClearScreen";
            this.ClearScreen.Size = new System.Drawing.Size(67, 30);
            this.ClearScreen.TabIndex = 22;
            this.ClearScreen.Text = "Clear";
            this.ClearScreen.UseVisualStyleBackColor = true;
            this.ClearScreen.Click += new System.EventHandler(this.ClearScreen_Click);
            // 
            // StopScriptButton
            // 
            this.StopScriptButton.Location = new System.Drawing.Point(539, 12);
            this.StopScriptButton.Name = "StopScriptButton";
            this.StopScriptButton.Size = new System.Drawing.Size(75, 31);
            this.StopScriptButton.TabIndex = 23;
            this.StopScriptButton.Text = "Stop script";
            this.StopScriptButton.UseVisualStyleBackColor = true;
            this.StopScriptButton.Click += new System.EventHandler(this.StopScriptButton_Click);
            // 
            // ascii
            // 
            this.ascii.AutoSize = true;
            this.ascii.Location = new System.Drawing.Point(608, 582);
            this.ascii.Name = "ascii";
            this.ascii.Size = new System.Drawing.Size(52, 17);
            this.ascii.TabIndex = 24;
            this.ascii.TabStop = true;
            this.ascii.Text = "ASCII";
            this.ascii.UseVisualStyleBackColor = true;
            this.ascii.CheckedChanged += new System.EventHandler(this.ascii_CheckedChanged);
            // 
            // hex
            // 
            this.hex.AutoSize = true;
            this.hex.Location = new System.Drawing.Point(666, 580);
            this.hex.Name = "hex";
            this.hex.Size = new System.Drawing.Size(47, 17);
            this.hex.TabIndex = 25;
            this.hex.TabStop = true;
            this.hex.Text = "HEX";
            this.hex.UseVisualStyleBackColor = true;
            this.hex.CheckedChanged += new System.EventHandler(this.hex_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 624);
            this.Controls.Add(this.hex);
            this.Controls.Add(this.ascii);
            this.Controls.Add(this.StopScriptButton);
            this.Controls.Add(this.ClearScreen);
            this.Controls.Add(this.CRLFlabel);
            this.Controls.Add(this.AppendCRLF);
            this.Controls.Add(this.HostNameLabel);
            this.Controls.Add(this.ConsInputLabel);
            this.Controls.Add(this.SensConsCommand);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ConnSettings);
            this.Controls.Add(this.Log_button);
            this.Controls.Add(this.DiagLabel);
            this.Controls.Add(this.ConsOutput);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.FileOpen);
            this.Name = "Form1";
            this.Text = "Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button FileOpen;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.RichTextBox ConsOutput;
        private System.Windows.Forms.Label DiagLabel;
        private System.Windows.Forms.Button Log_button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button ConnSettings;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SensConsCommand;
        private System.Windows.Forms.Label ConsInputLabel;
        private System.Windows.Forms.Label HostNameLabel;
        private System.Windows.Forms.CheckBox AppendCRLF;
        private System.Windows.Forms.Label CRLFlabel;
        private System.Windows.Forms.Button ClearScreen;
        private System.Windows.Forms.Button StopScriptButton;
        private System.Windows.Forms.RadioButton ascii;
        private System.Windows.Forms.RadioButton hex;
     }
}

