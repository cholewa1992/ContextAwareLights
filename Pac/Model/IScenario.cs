using System.Collections.Generic;

namespace Pac.Model
{
    internal interface IScenario : IDevice
    {
        Zone Zone { get; set; }
        IList<IDevice> Devices { get; set; } 

    }
}