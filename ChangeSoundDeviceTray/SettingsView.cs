using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeSoundDeviceTray
{
    public partial class SettingsView : Form
    {
        public bool isShown;
        MainApplicationContext MainApplicationContext;
        const string devicesToolTip = "Wybierz urządzenie, które będzie ustawiało się na domyślne w momencie podłączenia";
        bool changeDefaultDevice = true;
        Properties.Settings defaultSettings;

        public SettingsView(MainApplicationContext mainApplicationContext)
        {
            InitializeComponent();
            MainApplicationContext = mainApplicationContext;
            defaultSettings = new Properties.Settings();
            SetToolTips();
        }

        private void SetDevicesCombobox()
        {
            comboBox_devices.Items.Clear();
            comboBox_devices.Items.Add("");
            foreach(var device in MainApplicationContext.Devices)
            {
                comboBox_devices.Items.Add(device.InterfaceName);
            }
        }

        private void SetToolTips()
        {
            toolTip1.SetToolTip(label_devices, devicesToolTip);
            toolTip1.SetToolTip(label_defaultDevice, devicesToolTip);
            toolTip1.SetToolTip(comboBox_devices, devicesToolTip);
        }

        private void LoadSettings(Properties.Settings settings)
        {
            checkBox_startWithWindows.Checked = settings.startWithWindows;

            label_defaultDevice.Text = settings.defaultDevice;
            int index = comboBox_devices.FindStringExact(label_defaultDevice.Text);
            changeDefaultDevice = false;
            if (index != -1)
                comboBox_devices.SelectedIndex = index;
            else
                comboBox_devices.SelectedIndex = 0;
            changeDefaultDevice = true;
        }

        public new void Show()
        {
            isShown = true;
            SetDevicesCombobox();
            SetDefaultSettings();
            LoadSettings(Properties.Settings.Default);
            base.Show();
        }

        private void SetDefaultSettings()
        {
            defaultSettings.startWithWindows = Properties.Settings.Default.startWithWindows;
            defaultSettings.defaultDevice = Properties.Settings.Default.defaultDevice;
        }

        public new void Hide()
        {
            isShown = false;
            base.Hide();
        }

        private void button_saveSettings_Click(object sender, EventArgs e)
        {
            if(
                ChangeSettings_StartWithWindows(Properties.Settings.Default.startWithWindows)
                )
            {
                Properties.Settings.Default.defaultDevice = comboBox_devices.SelectedItem.ToString();
                Properties.Settings.Default.Save();
            }
            Hide();
        }
        private void button_undoChanges_Click(object sender, EventArgs e)
        {
            LoadSettings(defaultSettings);
        }
        private void button_exit_Click(object sender, EventArgs e)
        {
            Hide();
            MainApplicationContext.Exit();
        }

        private bool ChangeSettings_StartWithWindows(bool startWithWindows)
        {
            Properties.Settings.Default.startWithWindows = checkBox_startWithWindows.Checked;
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (startWithWindows)
                    {
                        key.SetValue(Application.ProductName, Application.ExecutablePath);
                    }
                    else
                    {
                        key.DeleteValue(Application.ProductName, false);
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Nie udało się zapisać ustawień\n" + e.Message, "Błąd zapisu ustawień", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void comboBox_devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!changeDefaultDevice)
                return;
            label_defaultDevice.Text = comboBox_devices.SelectedItem.ToString();
        }
    }
}
