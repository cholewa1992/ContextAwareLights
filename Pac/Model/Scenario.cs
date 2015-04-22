using System;
using System.Collections.Generic;
using System.ComponentModel;
using ubilight;

namespace Pac.Model
{
    class Scenario : IScenario
    {

        public Zone Zone { get; set; }
        public IList<IDevice> Devices { get; set; }

        public string Identifier { get; set; }

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
