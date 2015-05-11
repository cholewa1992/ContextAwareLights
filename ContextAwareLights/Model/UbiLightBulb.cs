using System;
using ubilight;

namespace ContextAwareLights.Model
{
    public class UbiLightBulb : IDevice
    {
        public static Ubilight Ubilight;
        private int _lightLevel;
        public string Identifier { get; set; }

        public int LightLevel
        {
            get { return _lightLevel; }
            set
            {
                if(value < 0 || value > 100) throw new InvalidOperationException("LightLevel has to be or be between 0 and 100");
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
            if(LightLevel == 0){ 
                Off();
                return; 
            }
            Ubilight.SetLvel(Identifier, (double) LightLevel / 100);
            On();
        }

        public override bool Equals(object obj)
        {
            var other = obj as UbiLightBulb;
            return other != null && Identifier == other.Identifier;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}