using System;
using System.Collections.Generic;
using System.Linq;

namespace ContextAwareLights.Model
{
    public class Zone : IZone
    {
        #region Private fields

        private double _accuracy = 1;

        private HashSet<Beacon> _priorBeacons = new HashSet<Beacon>();

        #endregion

        public Zone()
        {
            Include = new HashSet<Beacon>();
            Exclude = new HashSet<Beacon>();
        }

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

        public HashSet<Beacon> Include { get; set; }

        public HashSet<Beacon> Exclude { get; set; }

        #endregion


        #region Public methods

        public virtual bool InZone(Person person)
        {
            if (Include == null || person == null) return false;

            foreach (var beaconCondition in Exclude)
            {
                var beacon = person.Beacons.SingleOrDefault(b => b.Equals(beaconCondition));
                if (beacon != null && beacon.Distance < beaconCondition.Distance)
                    return false;

            }
                
            var comp = Math.Ceiling(Include.Count*Accuracy);
            int i = 0;
            var tempList = new HashSet<Beacon>();

            foreach (var beaconCondition in Include)
            {
                var beacon =
                    person.Beacons.SingleOrDefault(b => b.Equals(beaconCondition));

                if (beacon != null)
                {
                    if (_priorBeacons.Contains(beacon))
                    {
                        if (beacon.Distance < beaconCondition.Distance + 0.5)
                        {
                            i++;
                            tempList.Add(beacon);
                        }
                    }
                    else
                    {
                        if (beacon.Distance < beaconCondition.Distance)
                        {
                            i++;
                            tempList.Add(beacon);
                        }
                    }
                }

                _priorBeacons = tempList;
            }
            return comp <= i;
        }

        #endregion
    }
}