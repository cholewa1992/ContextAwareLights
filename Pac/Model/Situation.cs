using System.Collections.Generic;
using ubilight;
using ubilight.LightingSystems;

namespace Pac.Model
{
    class Situation : ISituation
    {
        private readonly string[] _lightIDs;
        private Ubilight _ubi;

        public Zone Zone { get; set; }
        public string Name { get; set; }

        public Situation(Zone zone, string[] lightIDs, Ubilight ubilight)
        {
            _lightIDs = lightIDs;
            Zone = zone;
            _ubi = ubilight;
        }
        
        public void On()
        {
            foreach (var lightID in _lightIDs)
            {
                _ubi.TurnOn(lightID);
            }
        }

        public void Off()
        {
            foreach (var lightID in _lightIDs)
            {
                _ubi.TurnOff(lightID);
            }
        }

    }
}
