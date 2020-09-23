namespace subnetscan2nd
{
    partial class FormInfo
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
            this.comboBoxWin32API = new System.Windows.Forms.ComboBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.dgvWMI = new System.Windows.Forms.DataGridView();
            this.HostName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWMI)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxWin32API
            // 
            this.comboBoxWin32API.FormattingEnabled = true;
            this.comboBoxWin32API.Items.AddRange(new object[] {
            "Win32_ComputerSystem",
            "Win32_DiskDrive",
            "Win32_OperatingSystem",
            "Win32_Processor",
            "Win32_SystemDevices"});
            this.comboBoxWin32API.Location = new System.Drawing.Point(12, 12);
            this.comboBoxWin32API.Name = "comboBoxWin32API";
            this.comboBoxWin32API.Size = new System.Drawing.Size(342, 21);
            this.comboBoxWin32API.TabIndex = 0;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(360, 10);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // dgvWMI
            // 
            this.dgvWMI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWMI.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvWMI.Location = new System.Drawing.Point(0, 39);
            this.dgvWMI.Name = "dgvWMI";
            this.dgvWMI.Size = new System.Drawing.Size(582, 299);
            this.dgvWMI.TabIndex = 2;
            // 
            // HostName
            // 
            this.HostName.AutoSize = true;
            this.HostName.Location = new System.Drawing.Point(427, 91);
            this.HostName.Name = "HostName";
            this.HostName.Size = new System.Drawing.Size(35, 13);
            this.HostName.TabIndex = 3;
            this.HostName.Text = "label1";
            this.HostName.Visible = false;
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 338);
            this.Controls.Add(this.HostName);
            this.Controls.Add(this.dgvWMI);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.comboBoxWin32API);
            this.Name = "FormInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get System Information";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWMI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxWin32API;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.DataGridView dgvWMI;
        public System.Windows.Forms.Label HostName;
    }
}

