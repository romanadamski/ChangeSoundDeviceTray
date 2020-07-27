namespace ChangeSoundDeviceTray
{
    partial class SettingsView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this.checkBox_startWithWindows = new System.Windows.Forms.CheckBox();
            this.button_saveSettings = new System.Windows.Forms.Button();
            this.comboBox_devices = new System.Windows.Forms.ComboBox();
            this.label_devices = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label_defaultDevice = new System.Windows.Forms.Label();
            this.button_undoChanges = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_startWithWindows
            // 
            this.checkBox_startWithWindows.AutoSize = true;
            this.checkBox_startWithWindows.Location = new System.Drawing.Point(30, 37);
            this.checkBox_startWithWindows.Name = "checkBox_startWithWindows";
            this.checkBox_startWithWindows.Size = new System.Drawing.Size(176, 17);
            this.checkBox_startWithWindows.TabIndex = 0;
            this.checkBox_startWithWindows.Text = "Uruchamiaj przy starcie systemu";
            this.checkBox_startWithWindows.UseVisualStyleBackColor = true;
            this.checkBox_startWithWindows.CheckedChanged += new System.EventHandler(this.checkBox_startWithWindows_CheckedChanged);
            // 
            // button_saveSettings
            // 
            this.button_saveSettings.Location = new System.Drawing.Point(331, 204);
            this.button_saveSettings.Name = "button_saveSettings";
            this.button_saveSettings.Size = new System.Drawing.Size(75, 23);
            this.button_saveSettings.TabIndex = 1;
            this.button_saveSettings.Text = "Zapisz";
            this.button_saveSettings.UseVisualStyleBackColor = true;
            this.button_saveSettings.Click += new System.EventHandler(this.button_saveSettings_Click);
            // 
            // comboBox_devices
            // 
            this.comboBox_devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_devices.FormattingEnabled = true;
            this.comboBox_devices.Location = new System.Drawing.Point(30, 85);
            this.comboBox_devices.Name = "comboBox_devices";
            this.comboBox_devices.Size = new System.Drawing.Size(313, 21);
            this.comboBox_devices.TabIndex = 2;
            this.comboBox_devices.SelectedIndexChanged += new System.EventHandler(this.comboBox_devices_SelectedIndexChanged);
            // 
            // label_devices
            // 
            this.label_devices.AutoSize = true;
            this.label_devices.Location = new System.Drawing.Point(27, 69);
            this.label_devices.Name = "label_devices";
            this.label_devices.Size = new System.Drawing.Size(110, 13);
            this.label_devices.TabIndex = 3;
            this.label_devices.Text = "Urządzenie domyślne:";
            // 
            // label_defaultDevice
            // 
            this.label_defaultDevice.AutoSize = true;
            this.label_defaultDevice.Location = new System.Drawing.Point(144, 69);
            this.label_defaultDevice.Name = "label_defaultDevice";
            this.label_defaultDevice.Size = new System.Drawing.Size(0, 13);
            this.label_defaultDevice.TabIndex = 4;
            // 
            // button_undoChanges
            // 
            this.button_undoChanges.Location = new System.Drawing.Point(239, 204);
            this.button_undoChanges.Name = "button_undoChanges";
            this.button_undoChanges.Size = new System.Drawing.Size(86, 23);
            this.button_undoChanges.TabIndex = 5;
            this.button_undoChanges.Text = "Cofnij zmiany";
            this.button_undoChanges.UseVisualStyleBackColor = true;
            this.button_undoChanges.Click += new System.EventHandler(this.button_undoChanges_Click);
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 239);
            this.Controls.Add(this.button_undoChanges);
            this.Controls.Add(this.label_defaultDevice);
            this.Controls.Add(this.label_devices);
            this.Controls.Add(this.comboBox_devices);
            this.Controls.Add(this.button_saveSettings);
            this.Controls.Add(this.checkBox_startWithWindows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ustawienia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_startWithWindows;
        private System.Windows.Forms.Button button_saveSettings;
        private System.Windows.Forms.ComboBox comboBox_devices;
        private System.Windows.Forms.Label label_devices;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label_defaultDevice;
        private System.Windows.Forms.Button button_undoChanges;
    }
}