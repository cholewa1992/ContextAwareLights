using System;
using System.Collections.Generic;
using Ocon.Entity;

namespace ContextAwareLights.Model
{
    public class Person : IEntity
    {
        public HashSet<Beacon> Beacons { get; set; }
        public Guid Id { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}