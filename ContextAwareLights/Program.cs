using System;
using System.Collections.Generic;
using System.Linq;
using ContextAwareLights.Model;
using LightBulbs.LightingSystems;
using Ocon;
using Ocon.OconCommunication;
using Ocon.OconSerializer;

namespace ContextAwareLights
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var program = new Program();
            Console.ReadLine();
        }

        private readonly Cal _pac;

        public Program()
        {
            _pac = new Cal();
            _pac.AutoOffAfter(20);
            SetupOcon();
            LightBulb.LightBulbs = new LightBulbs.LightBulbs(new List<ILightingSystem> { new HueLightingSystem("hue", "169.254.2.185") });

            var spisebord = new Zone
            {
                Accuracy = 0.5,
                Include = new HashSet<Beacon>
                {
                    new Beacon {Major = 5000, Minor = 4242, Distance = 3},
                    new Beacon {Major = 5000, Minor = 3927, Distance = 3}
                }
            };

            var køkken = new Zone
            {
                Accuracy = 0.5,
                Include = new HashSet<Beacon>
                {
                    new Beacon {Major = 5000, Minor = 3373, Distance = 10},
                    new Beacon {Major = 5000, Minor = 2304, Distance = 10}
                }
            };

            var sofabord = new Zone
            {
                Accuracy = 0.5,
                Include = new HashSet<Beacon>
                {
                    new Beacon {Major = 5000, Minor = 4325, Distance = 2},
                    new Beacon {Major = 5000, Minor = 4286, Distance = 2}
                }
            };

            var stue = new Zone
            {
                Accuracy = 0.50,
                Include = new HashSet<Beacon>
                {
                    new Beacon {Major = 5000, Minor = 4325, Distance = 15},
                    new Beacon {Major = 5000, Minor = 4242, Distance = 15},
                    new Beacon {Major = 5000, Minor = 4286, Distance = 15},
                    new Beacon {Major = 5000, Minor = 3927, Distance = 15}
                }
            };





            //hue1 = spisebord (4242)
            //hue2 = køkken (3373)
            //hue3 = sofabord (4325)

            
            _pac.AddScenario(new Scenario
            {
                Zone = stue,
                Devices = new HashSet<IDevice>
                {
                    new LightBulb("hue1") {LightLevel = 25},
                    new LightBulb("hue2") {LightLevel = 0},
                    new LightBulb("hue3") {LightLevel = 25}
                },
            });

            _pac.AddScenario(new Scenario
            {
                Zone = sofabord,
                Priority = 1,
                Devices = new HashSet<IDevice>
                {
                    new LightBulb("hue1") {LightLevel = 1},
                    new LightBulb("hue2") {LightLevel = 0,},
                    new LightBulb("hue3") {LightLevel = 100}
                },
            });

            _pac.AddScenario(new Scenario
            {
                Priority = 1,
                Zone = køkken,
                Devices = new HashSet<IDevice>
                {
                    new LightBulb("hue1") {LightLevel = 1},
                    new LightBulb("hue2") {LightLevel = 100},
                    new LightBulb("hue3") {LightLevel = 0,}
                }
            });

            _pac.AddScenario(new Scenario
            {
                Zone = spisebord,
                Priority = 1,
                Devices = new HashSet<IDevice>
                {
                    new LightBulb("hue1") {LightLevel = 100,},
                    new LightBulb("hue2") {LightLevel = 0},
                    new LightBulb("hue3") {LightLevel = 1}
                }
            });
        }

        public void SetupOcon()
        {
            var comHelper = new OconComHelper(new CustomeCom(new JsonNetAdapter()));
            var client = new OconClient(comHelper);
            client.Subscribe(new Situation<ComparableCollection<Person>>(c => new ComparableCollection<Person>(c.OfType<Person>().Where(p => p.LastUpdate.AddSeconds(20) > DateTime.UtcNow))));
            client.SituationStateChangedEvent += situation => _pac.ActOnPeoplePresent(((Situation<ComparableCollection<Person>>)situation).Value);
            var central = new OconCentral(new OconContextFilter(), comHelper);

            comHelper.Broadcast(DeviceType.Central, 5);


            client.SituationStateChangedEvent += situation =>
            {
                Console.Clear();
                var s = (Situation<ComparableCollection<Person>>) situation;

                foreach (var person in s.Value)
                {
                    Console.WriteLine(person.Id);
                    var i = 0;
                    foreach (var beacon in person.Beacons)
                    {
                        Console.WriteLine("\t{2}: Beacon {0} is {1} meters away", beacon.Minor, beacon.Distance, ++i);
                    }
 
                }
            };
        }
    }
}