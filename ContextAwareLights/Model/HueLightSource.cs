using System;
using LightBulbs.LightingSystems;
using Q42.HueApi;

namespace ContextAwareLights.Model
{
    public class HueLightSource : ILightSource
    {
        static readonly HueClient Client = HueLightingSystem.GetInstance("169.254.2.185");
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

        public HueLightSource(string identifier)
        {
            Identifier = identifier;
            LightLevel = 100;
        }

        public void On()
        {
            byte b = Convert.ToByte(LightLevel * 254);

            Client.SendCommandAsync(new LightCommand { Brightness = b }, new[] { Identifier });

            Client.SendCommandAsync(new LightCommand().TurnOn(), new[] { Identifier });
        }

        public void Off()
        {
            Client.SendCommandAsync(new LightCommand().TurnOff(), new[] { Identifier });
        }

        public void Restore()
        {
            if(LightLevel == 0){ 
                Off();
                return; 
            }
            
            On();
        }

        public override bool Equals(object obj)
        {
            var other = obj as HueLightSource;
            return other != null && Identifier == other.Identifier;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}