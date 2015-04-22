using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ubilight;
using ubilight.LightingSystems;
using Pac;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ubi = new Ubilight(new List<ILightingSystem>(){new HueLightingSystem("hue")});

            var pac = new Pac.Pac();

            pac.AddSituation(new Pac.Model.Situation());
        }
    }
}
