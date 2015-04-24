using System;
using System.Security.Permissions;
using Ocon.Entity;

namespace Pac.Model
{
    public class Person : IEntity
    {
        public Beacon[] Beacons { get; set; }
        public Guid Id { get; set; }
<<<<<<< HEAD
        public Guid WidgetId { get; set; }
=======
        public DateTime LastUpdate { get; set; }
>>>>>>> 9a76cc96e3a9d49e064d006e900ad4d992148cbc
    }
}