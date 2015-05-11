using System.Collections.Generic;

namespace ContextAwareLights.Model
{
    public interface IScenario
    {
        int Priority { get; set; }
        Zone Zone { get; set; }
        ISet<IDevice> Devices { get; set; }
    }
}