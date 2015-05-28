using System.Collections.Generic;

namespace ContextAwareLights.Model
{
    public class Scenario : IScenario
    {
        public int Priority { get; set; }
        public Zone Zone { get; set; }
        public ISet<ILightSource> Devices { get; set; }
        public string Identifier { get; set; }
    }
}
