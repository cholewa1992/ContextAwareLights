using System.Collections.Generic;

namespace ContextAwareLights.Model
{
    public interface IScenario
    {
        int Priority { get; set; }
        Zone Zone { get; set; }
        ISet<ILightSource> Devices { get; set; }
    }
}