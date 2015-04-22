using System;
using System.Collections.Generic;
using System.Linq;

namespace Pac.Model
{
    public class Zone
    {
        #region Private fields
        private double _accuracy = 1;
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
                if (value > 1 || value < 0.01) throw new InvalidOperationException("The accuracy must be between 1 and 0.01");
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
        public bool InZone(Person person)
        {
            return  Signature != null && person != null && Math.Ceiling(Signature.Count * Accuracy) <= Signature.Count(beacon => person.Beacons.Contains(beacon, BeaconEquallityCompare.GetInstace()));
        }
        #endregion
    }
}