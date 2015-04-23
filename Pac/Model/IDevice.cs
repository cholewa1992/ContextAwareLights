using System;
using System.Collections.Generic;
using ubilight;
using ubilight.LightingSystems;

namespace Pac.Model
{
    public interface IDevice
    {
        void On();
        void Off();
        void Restore();
    }


    public class UbiLightBulb : IDevice
    {
        private static readonly Ubilight Ubilight = new Ubilight(new List<ILightingSystem> {new HueLightingSystem("hue", "130.226.141.243")});
        private int _lightLevel;
        public string Identifier { get; set; }

        public int LightLevel
        {
            get { return _lightLevel; }
            set
            {
                if(value < 0 || value > 100) throw new InvalidOperationException("LightLevel has to be between 0 and 100");
                _lightLevel = value;
            }
        }

        public UbiLightBulb(string identifier)
        {
            Identifier = identifier;
            LightLevel = 100;
        }

        public void On()
        {
            Ubilight.TurnOn(Identifier);
        }

        public void Off()
        {
            Ubilight.TurnOff(Identifier);
        }

        public void Restore()
        {
            Ubilight.SetLvel(Identifier, (double) LightLevel / 100);
            On();
        }
    }
}
