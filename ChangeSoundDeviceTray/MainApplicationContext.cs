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

namespace ChangeSoundDeviceTray
{
    public class MainApplicationContext : ApplicationContext
    {
        NotifyIcon trayIcon;
        ContextMenu contextMenu;
        List<CoreAudioDevice> Devices;
        List<MenuItem> ContextMenuItems;
        CoreAudioController AudioController;
        DeviceObserver deviceObserver;
        public MainApplicationContext()
        {
            Icon myIcon = PrepareIcon();
            contextMenu = new ContextMenu();
            ContextMenuItems = new List<MenuItem>();
            Devices = new List<CoreAudioDevice>();
            AudioController = new CoreAudioController();
            deviceObserver = new DeviceObserver(this);
            AudioController.AudioDeviceChanged.Subscribe(deviceObserver);
            PrepareContextMenu();
            trayIcon = new NotifyIcon()
            {
                Icon = myIcon,
                ContextMenu = contextMenu,
                Visible = true
            };
        }
        public void RefreshContextMenu()
        {
            PrepareContextMenu();
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
                MenuItem menuItemDevice = new MenuItem(device.InterfaceName, onDeviceClick);
                menuItemDevice.Tag = device;
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
        void onDeviceClick (object sender, EventArgs e)
        {
            if (!(sender is MenuItem))
                return;

            var menuItem = sender as MenuItem;
            CoreAudioDevice selectedAudioDevice = menuItem.Tag as CoreAudioDevice;
            bool isDefDeviceSet = selectedAudioDevice.SetAsDefault();
            if(isDefDeviceSet)
                selectedAudioDevice.SetAsDefaultCommunications();
            if (isDefDeviceSet)
            {

            }
        }
        private static Icon PrepareIcon()
        {
            Bitmap bmpIcon = new Bitmap(Resources.AppIcon);
            IntPtr Hicon = bmpIcon.GetHicon();
            Icon myIcon = Icon.FromHandle(Hicon);
            return myIcon;
        }
        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
