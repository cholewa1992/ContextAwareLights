using System;

namespace ContextAwareLights.Model
{
    public class Beacon : IComparable<Beacon>
    {
        public Guid Id { get; set; }
        public int Rssi { get; set; }
        public double Distance { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }

        public int CompareTo(Beacon other)
        {
            if (Major == other.Major && Minor == other.Minor)
            {
                return Distance < other.Distance ? 0 : 1;
            }
            return -1;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Beacon;
            return other != null && GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (Major << Minor).GetHashCode();
        }
    }
}