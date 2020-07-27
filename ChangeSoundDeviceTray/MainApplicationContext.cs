using ChangeSoundDeviceTray.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Reflection;
using System.Threading;

namespace ChangeSoundDeviceTray
{
    public class MainApplicationContext : ApplicationContext
    {
        NotifyIcon trayIcon;
        ContextMenu contextMenu;
        public List<CoreAudioDevice> Devices;
        List<MenuItem> ContextMenuItems;
        CoreAudioController AudioController;
        DeviceObserver deviceObserver;
        SettingsView SettingsView;

        public MainApplicationContext()
        {
            Icon myIcon = PrepareIcon();
            PrepareObjects();
            AudioController.AudioDeviceChanged.Subscribe(deviceObserver);
            PrepareContextMenu();
            PrepareNotifyIcon(myIcon);
        }

        private void PrepareObjects()
        {
            contextMenu = new ContextMenu();
            ContextMenuItems = new List<MenuItem>();
            Devices = new List<CoreAudioDevice>();
            AudioController = new CoreAudioController();
            deviceObserver = new DeviceObserver(this);
            SettingsView = new SettingsView(this);
            SettingsView.FormClosing += SettingsView_FormClosing;
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            SettingsView.Hide();
        }

        private void PrepareNotifyIcon(Icon myIcon)
        {
            trayIcon = new NotifyIcon()
            {
                Icon = myIcon,
                ContextMenu = contextMenu,
                Visible = true,
            };
            trayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
            trayIcon.DoubleClick += TrayIcon_DoubleClick;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            SettingsView.Show();
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            var notifyIcon = sender as NotifyIcon;
            if (notifyIcon.BalloonTipTitle.Equals(BalloonTipConsts.DEVICE_ADDED))
            {
                SetDefaultDeviceByName(notifyIcon.BalloonTipText);
            }
        }

        public void RefreshContextMenu()
        {
            PrepareContextMenu();
        }
        public void NotifyAddedDevice(string deviceName)
        {
            EventHandler temp = DoNotifyAddedDevice;
            temp?.Invoke(deviceName, null);
        }

        private void DoNotifyAddedDevice(object sender, EventArgs e)
        {
            ShowBalloonToolTip(BalloonTipConsts.DEVICE_ADDED, sender.ToString(), ToolTipIcon.Info, BalloonTipConsts.TIMEOUT);
        }
        public void SetDefaultDeviceBySettings()
        {
            SetDefaultDeviceByName(Settings.Default.defaultDevice);
        }
        private void ShowBalloonToolTip(string balloonToolTipTitle, string deviceName, ToolTipIcon toolTipIcon, int timeout)
        {
            trayIcon.BalloonTipTitle = balloonToolTipTitle;
            trayIcon.BalloonTipText = deviceName;
            trayIcon.BalloonTipIcon = toolTipIcon;
            trayIcon.ShowBalloonTip(timeout);
        }

        List<CoreAudioDevice> GetAllValidSoundDevices()
        {
            var devices = AudioController.GetDevices();
            return devices.Where(x => x.IsPlaybackDevice && x.State == DeviceState.Active).ToList();
        }
        private void PrepareContextMenu()
        {
            GetAllContextMenuItems();
        }

        private void GetAllContextMenuItems()
        {
            GetAllSoundDevicesForContextMenu();
        }

        void GetAllSoundDevicesForContextMenu()
        {
            ContextMenuItems.Clear();
            contextMenu.MenuItems.Clear();
            Devices.Clear();

            Devices = GetAllValidSoundDevices();
            foreach (var device in Devices.ToList())
            {
                MenuItem menuItemDevice = new MenuItem(device.InterfaceName, OnDeviceClick)
                {
                    Tag = device
                };
                ContextMenuItems.Add(menuItemDevice);
                contextMenu.MenuItems.AddRange(ContextMenuItems.ToArray());
            }
            var defaultDevice = AudioController.GetDefaultDevice(DeviceType.Playback, Role.Multimedia);
            MenuItem menuItemDefault = ContextMenuItems.Find(x => ((CoreAudioDevice)x.Tag).FullName == defaultDevice.FullName);
            if (menuItemDefault != null)
            {
                menuItemDefault.Checked = true;
            }
        }
        void OnDeviceClick (object sender, EventArgs e)
        {
            if (!(sender is MenuItem))
                return;

            var menuItem = sender as MenuItem;
            CoreAudioDevice selectedAudioDevice = menuItem.Tag as CoreAudioDevice;
            SetDefaultDeviceByObject(selectedAudioDevice);
        }
        void SetDefaultDeviceByName(string name)
        {
            var device = Devices.Find(x => x.InterfaceName == name);
            if(device != null)
                SetDefaultDeviceByObject(device);
        }

        bool SetDefaultDeviceByObject(CoreAudioDevice coreAudioDevice)
        {
            bool isDefDeviceSet = coreAudioDevice.SetAsDefault();
            if (isDefDeviceSet)
                coreAudioDevice.SetAsDefaultCommunications();
            if (isDefDeviceSet)
            {
                return true;
            }
            return false;
        }
        private static Icon PrepareIcon()
        {
            Bitmap bmpIcon = new Bitmap(Resources.AppIcon);
            IntPtr Hicon = bmpIcon.GetHicon();
            Icon myIcon = Icon.FromHandle(Hicon);
            return myIcon;
        }
        
    }
}
