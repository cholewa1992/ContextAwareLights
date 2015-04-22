using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Threading;
using Pac.Model;
using ubilight;
using ubilight.LightingSystems;

namespace Pac
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pac = new Pac();

            var zone = new Zone(new Beacon { Major = 5000, Minor = 3927, Proximity = Proximity.Near });

            var ubilight = new Ubilight(new List<ILightingSystem>() { new HueLightingSystem("hue") });

            pac.AddSituation(new Situation(zone, new[] { "hue1", "hue2", "hue3" }, ubilight));

            

            bool on = false;

            

            do
            {

                if (on)
                {
                    var people = new Collection<Person>()
                    {
                        new Person()
                        {
                            Beacons = new Beacon[]
                            {
                                new Beacon()
                                {
                                    Major = 5000,
                                    Minor = 3927,
                                    Proximity = Proximity.Far
                                }
                            }
                        }
                    };

                    pac.ActOnPeoplePresent(people);
                    Console.WriteLine("on");
                    on = false;
                }
                else
                {
                    var people = new Collection<Person>()
                    {
                        new Person()
                        {
                            Beacons = new Beacon[]
                            {
                                new Beacon()
                                {
                                    Major = 5000,
                                    Minor = 3927,
                                    Proximity = Proximity.Near
                                }
                            }
                        }
                    };

                    pac.ActOnPeoplePresent(people);
                    Console.WriteLine("off");
                    on = true;
                }

                Thread.Sleep(2000);

            } while (true);
        }
    }
}