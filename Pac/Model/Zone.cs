using System;
using System.Collections.Generic;
using System.Linq;

namespace Pac.Model
{
    public class Zone
    {
        #region Private fields

        private double _accuracy = 1;

        private List<Beacon> _priorBeacons;

        #endregion

        #region Properties

        /// <summary>
        /// The accuracy is a double between 1 and 0.01 which is the procent of beacons that have to be present for a person to be in the zone
        /// </summary>
        public double Accuracy
        {
            get { return _accuracy; }
            set
            {
                if (value > 1 || value < 0.01)
                    throw new InvalidOperationException("The accuracy must be between 1 and 0.01");
                _accuracy = value;
            }
        }

        /// <summary>
        /// The common name of the zone
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The signature of the zone
        /// </summary>
        public List<Beacon> Signature { get; set; }

        #endregion


        #region Public methods

        public virtual bool InZone(Person person)
        {
            if (Signature == null || person == null) return false;

            var comp = Math.Ceiling(Signature.Count*Accuracy);

            int i = 0;

            var tempList = new List<Beacon>();

            foreach (var beacon in Signature)
            {

                double distance;
                Beacon beacon2 = beacon;

                
                var sBeacon =
                    person.Beacons.Where(beacon1 => BeaconEquallityCompare.GetInstace().Equals(beacon2, beacon1))
                        .Select(b => b)
                        .First();

                if (sBeacon != null)
                {
                    if (_priorBeacons.Contains(sBeacon, BeaconEquallityCompare.GetInstace()))
                    {
                        if (sBeacon.Distance < beacon.Distance + 1)
                        {
                            comp++;
                        }
                    }
                    else
                    {
                        if (sBeacon.Distance < beacon.Distance)
                        {
                            comp++;
                        }
                    }
                }


                if (person.Beacons.Contains(beacon, BeaconEquallityCompare.GetInstace()) && person.Beacons.Where(beacon1 => beacon1.Equals(beacon)).Select(beacon1 => beacon1.))
                {
                    

                }

                if (person.Beacons.Contains(beacon, BeaconEquallityCompare.GetInstace())) i++;
            }

            return comp <= i;
        }

        #endregion
    }
}