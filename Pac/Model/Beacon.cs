using System;

namespace Pac.Model
{
    public class Beacon : IComparable<Beacon>
    {
		public Guid Id { get; set; }
		public int Rssi { get; set; }
		public double Distance { get; set; }
		public Proximity Proximity {get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }

        public int CompareTo(Beacon other)
        {
            if (Major == other.Major && Minor == other.Minor)
                return Proximity == other.Proximity ? 0 : 1;
            return -1;
        }
    }
}

