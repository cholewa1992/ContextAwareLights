using System;
using System.Security.Permissions;

namespace Pac.Model
{
    public class Person : ComparableEntity
    {
        public Guid Id { get; set; }
        public Beacon[] Beacons { get; set; }
    }
}