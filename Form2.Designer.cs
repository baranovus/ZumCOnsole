namespace ZumConsole
{
    partial class Form2
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HostName = new System.Windows.Forms.TextBox();
            this.PortName = new System.Windows.Forms.TextBox();
            this.IP_addr_label = new System.Windows.Forms.Label();
            this.Port_label = new System.Windows.Forms.Label();
            this.Set = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HostName
            // 
            this.HostName.Location = new System.Drawing.Point(63, 56);
            this.HostName.Name = "HostName";
            this.HostName.Size = new System.Drawing.Size(253, 20);
            this.HostName.TabIndex = 0;
            // 
            // PortName
            // 
            this.PortName.Location = new System.Drawing.Point(69, 123);
            this.PortName.Name = "PortName";
            this.PortName.Size = new System.Drawing.Size(246, 20);
            this.PortName.TabIndex = 1;
            // 
            // IP_addr_label
            // 
            this.IP_addr_label.AutoSize = true;
            this.IP_addr_label.Location = new System.Drawing.Point(66, 28);
            this.IP_addr_label.Name = "IP_addr_label";
            this.IP_addr_label.Size = new System.Drawing.Size(123, 13);
            this.IP_addr_label.TabIndex = 2;
            this.IP_addr_label.Text = "IP address or Host name";
            // 
            // Port_label
            // 
            this.Port_label.AutoSize = true;
            this.Port_label.Location = new System.Drawing.Point(71, 98);
            this.Port_label.Name = "Port_label";
            this.Port_label.Size = new System.Drawing.Size(26, 13);
            this.Port_label.TabIndex = 3;
            this.Port_label.Text = "Port";
            // 
            // Set
            // 
            this.Set.Location = new System.Drawing.Point(73, 185);
            this.Set.Name = "Set";
            this.Set.Size = new System.Drawing.Size(71, 41);
            this.Set.TabIndex = 4;
            this.Set.Text = "OK";
            this.Set.UseVisualStyleBackColor = true;
            this.Set.Click += new System.EventHandler(this.Set_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 262);
            this.Controls.Add(this.Set);
            this.Controls.Add(this.Port_label);
            this.Controls.Add(this.IP_addr_label);
            this.Controls.Add(this.PortName);
            this.Controls.Add(this.HostName);
            this.Name = "Form2";
            this.Text = "Communication Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox HostName;
        private System.Windows.Forms.TextBox PortName;
        private System.Windows.Forms.Label IP_addr_label;
        private System.Windows.Forms.Label Port_label;
        private System.Windows.Forms.Button Set;
    }
}