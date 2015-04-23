using System.Collections.Generic;

namespace Pac.Model
{
    public interface IScenario : IDevice
    {
        Zone Zone { get; set; }
        IList<IDevice> Devices { get; set; } 

    }
}