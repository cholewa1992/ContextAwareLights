using System.Collections.Generic;

namespace ContextAwareLights.Model
{
    public interface IZone
    {
        double Accuracy { get; set; }
        HashSet<Beacon> Include { get; set; }
        HashSet<Beacon> Exclude { get; set; }
        bool InZone(Person person);
    }
}