using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ubilight;
using ubilight.LightingSystems;

namespace Pac
{
    class Program
    {
        static void Main(string[] args)
        {
            var pac = new Pac();

            do {
                Console.Write(">");
            }
            while (Console.ReadLine() != "exit") ;
        }
    }
}
