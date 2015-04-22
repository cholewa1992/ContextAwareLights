using System.Collections.Generic;
using System.Linq;

namespace Pac.Model
{
    public class Zone
    {
        private static readonly BeaconEquallityCompare Comp = new BeaconEquallityCompare();

        public Zone(Beacon beacon, params Beacon[] beacons)
        {
            Signature = new List<Beacon> {beacon};
            Signature.AddRange(beacons);
        }

        public string Name { get; set; }
        public List<Beacon> Signature { get; set; }

        public bool InZone(Person person)
        {
            bool result = Signature.All(beacon => person.Beacons.Contains(beacon, Comp));
            return result;
        }
    }
}