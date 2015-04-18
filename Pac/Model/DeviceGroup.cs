using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pac.Annotations;

namespace Pac.Model
{
    class DeviceGroup : IDevice
    {
        public IList<IDevice> Devices { get; set; }
        public string Name { get; set; }

        public DeviceGroup()
        {
            Devices = new List<IDevice>();
        }

        public void On()
        {
            foreach (var device in Devices)
            {
                device.On();
            }
        }

        public void Off()
        {
            foreach (var device in Devices)
            {
                device.Off();
            }
        }

        public void Restore()
        {
            foreach (var device in Devices)
            {
                device.Restore();
            }
        }
    }
}
