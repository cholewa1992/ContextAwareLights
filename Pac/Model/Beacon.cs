using System;
using HueLightClient;

namespace Pac.Model
{
    public class Beacon
	{
		public Guid Id { get; set; }
		public int Rssi { get; set; }
		public double Distance { get; set; }
		public Proximity Proximity {get; set; }
	}
}

