using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi;

namespace ChangeSoundDeviceTray
{
    public class DeviceObserver : IObserver<DeviceChangedArgs>
    {
        MainApplicationContext mainApplicationContext;
        public DeviceObserver(MainApplicationContext mainApplicationContext)
        {
            this.mainApplicationContext = mainApplicationContext;
        }
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(DeviceChangedArgs value)
        {
            mainApplicationContext.RefreshContextMenu();
            if(value.ChangedType == DeviceChangedType.DeviceAdded)
            {
                mainApplicationContext.NotifyAddedDevice(value.Device.InterfaceName);
            }
        }
    }
}
