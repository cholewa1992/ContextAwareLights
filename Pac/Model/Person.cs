using System;
using System.Security.Permissions;
using Ocon.Entity;

namespace Pac.Model
{
    public class Person : IEntity
    {
        public Beacon[] Beacons { get; set; }

        public Guid Id { get; set; }
        public Guid WidgetId { get; set; }
    }
}