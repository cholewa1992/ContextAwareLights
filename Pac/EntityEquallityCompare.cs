using System.Collections.Generic;
using Ocon.Entity;
using Pac.Model;

namespace Pac
{
    public class EntityEquallityCompare<T> : IEqualityComparer<T> where T : IEntity
    {
        public bool Equals(T x, T y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(T obj)
        {
            return obj.WidgetId.GetHashCode()*obj.Id.GetHashCode();
        }
    }

    public class BeaconEquallityCompare : IEqualityComparer<Beacon>
    {
        private static BeaconEquallityCompare _instace;

        public static BeaconEquallityCompare GetInstace()
        {
            return _instace ?? (_instace = new BeaconEquallityCompare());
        }
        private BeaconEquallityCompare()
        {
            
        }

        public bool Equals(Beacon x, Beacon y)
        {
            return 0 == x.CompareTo(y);
        }

        public int GetHashCode(Beacon obj)
        {
            return (obj.Major << obj.Minor).GetHashCode();
        }
    }
}