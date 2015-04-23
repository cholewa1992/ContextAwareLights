using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Threading;
using Ocon;
using Ocon.OconCommunication;
using Ocon.OconSerializer;
using Ocon.TcpCom;
using Pac.Model;
using ubilight;
using ubilight.LightingSystems;

namespace Pac
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program();
            Console.ReadLine();
        }

        private readonly Pac _pac;

        public Program()
        {
            _pac = new Pac();
            Console.ReadLine();
            SetupOcon();

            var florian = new Zone
            {
                Signature = new List<Beacon> {new Beacon {Major = 5000, Minor = 4242, Distance = 3}}
            };
            var jacob = new Zone {Signature = new List<Beacon> {new Beacon {Major = 5000, Minor = 3373, Distance = 2}}};
            var mathias = new Zone
            {
                Signature = new List<Beacon> {new Beacon {Major = 5000, Minor = 4325, Distance = 2}}
            };


            _pac.AddSituation(new Scenario
            {
                Zone = florian,
                Devices = new List<IDevice>
                {
                    new UbiLightBulb("hue1")
                }
            });

            _pac.AddSituation(new Scenario
            {
                Zone = jacob,
                Devices = new List<IDevice>
                {
                    new UbiLightBulb("hue2") {LightLevel = 100},
                    new UbiLightBulb("hue3") {LightLevel = 20}
                }
            });

            _pac.AddSituation(new Scenario
            {
                Zone = mathias,
                Devices = new List<IDevice>
                {
                    new UbiLightBulb("hue3")
                }
            });
        }

        public void SetupOcon()
        {
            var comHelper = new OconComHelper(new TcpCom(new JsonNetAdapter()));
            var client = new OconClient(comHelper);
            client.Subscribe(new Situation<ComparableCollection<Person>>(c => new ComparableCollection<Person>(c.OfType<Person>())));
            client.SituationStateChangedEvent += situation => _pac.ActOnPeoplePresent(((Situation<ComparableCollection<Person>>)situation).Value);
            var central = new OconCentral(new OconContextFilter(), comHelper);

            comHelper.Broadcast(DeviceType.Central, 5);

            comHelper.EntityEvent += entity =>
            {
                Console.Clear();
                var person = (Person)entity;
                int i = 0;
                foreach (var b in person.Beacons)
                {
                    Console.WriteLine("{2}: Beacon {0} is {1} meters away", b.Minor, b.Distance, ++i);
                }
            };
        }
    }
}