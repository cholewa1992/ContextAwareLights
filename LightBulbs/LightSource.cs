using System.Drawing;
using LightBulbs.LightingSystems;

namespace LightBulbs
{
    public class LightSource
    {   


        public string SystemId { get; private set; }

        public LightSource(ILightingSystem ls, string systemId)
        {
            LightingSystem = ls;
            SystemId = systemId;
        }

        public bool On {
            get { return LightingSystem.GetOn(this); }
            set
            {
                if (value)
                {
                    LightingSystem.TurnOn(this);
                }
                else
                {
                    LightingSystem.TurnOff(this);
                }
            }
        }

        public Color Color
        {
            get { return LightingSystem.GetColor(this); }
            set
            {
                LightingSystem.SetColor(this, value);
            }
        }

        public double Level
        {
            get { return LightingSystem.GetLevel(this); }
            set{ LightingSystem.SetLevel(this, value); }
        }


        public ILightingSystem LightingSystem { get; private set; }
    }
}
