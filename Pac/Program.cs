using System;
using System.Collections.Generic;
using System.Linq;
using Ocon;
using Ocon.OconCommunication;
using Ocon.OconSerializer;
using Ocon.TcpCom;
using Pac.Model;

namespace Pac
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var program = new Program();
            Console.ReadLine();
        }

        private readonly Pac _pac;

        public Program()
        {
            _pac = new Pac();
            _pac.AutoOffAfter(20);
            SetupOcon();

            var florian = new Zone
            {
                Signature = new List<Beacon> {new Beacon {Major = 5000, Minor = 4242, Distance = 3}}
            };

            var jacob = new Zone
            {
                Signature = new List<Beacon> {new Beacon {Major = 5000, Minor = 3373, Distance = 2}}
            };

            var mathias = new Zone
            {
                Signature = new List<Beacon> {new Beacon {Major = 5000, Minor = 4325, Distance = 2}}
            };

            _pac.AddSituation(new Scenario
            {
                Zone = florian,
                Devices = new List<IDevice>{ new UbiLightBulb("hue1") }
            });

            _pac.AddSituation(new Scenario
            {
                Zone = jacob,
                Devices = new List<IDevice> {new UbiLightBulb("hue2")}
            });

            _pac.AddSituation(new Scenario
            {
                Zone = mathias,
                Devices = new List<IDevice> {new UbiLightBulb("hue3")}
            });
        }

        public void SetupOcon()
        {
            var comHelper = new OconComHelper(new TcpCom(new JsonNetAdapter()));
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
                    for(var i = 0; i < person.Beacons.Length; i++)
                    {
                        var beacon = person.Beacons[i];
                        Console.WriteLine("\t{2}: Beacon {0} is {1} meters away", beacon.Minor, beacon.Distance, i);
                    }
                }
            };
        }
    }
}