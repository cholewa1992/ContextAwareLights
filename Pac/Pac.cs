using System;
using System.Collections.Generic;
using System.Linq;
using Ocon;
using Ocon.TcpCom;
using Ocon.OconSerializer;
using Ocon.OconCommunication;
using Pac.Model;
using Zone = Pac.Model.Zone;


namespace Pac
{
    class Pac
    {
        private readonly IList<Situation> _situations = new List<Situation>();

        public Pac()
        {
            var comHelper = new OconComHelper(new TcpCom(new JsonNetAdapter()));
            var client = new OconClient(comHelper);
            client.Subscribe(new Situation<ComparableCollection<Person>>(c => new ComparableCollection<Person>(c.OfType<Person>())));
            client.SituationStateChangedEvent += situation => ActOnPeoplePresent( ((Situation<ComparableCollection<Person>>) situation).Value );

            var central = new OconCentral(new OconContextFilter(), comHelper);
            comHelper.Broadcast(DeviceType.Central, 5);

            comHelper.DiscoveryEvent += peer => Console.WriteLine("Found peer {0}", peer);

            var bulb = new HueBulb {LightLevel = 50};
            var zone = new Zone(new Beacon {Major = 5000, Minor = 3927, Proximity = Proximity.Near});


            AddSituation(new Situation(zone, new HueBulb()));
        }

        public void AddSituation(Situation situation)
        {
            _situations.Add(situation);
        }

        public void ActOnPeoplePresent(ICollection<Person> people)
        {
            Console.WriteLine(people.Count);
            foreach (var person in people)
            {
                foreach (var situation in _situations)
                {
                    if (situation.Zone.InZone(person))
                    {
                        situation.Restore();
                        Console.WriteLine("Restore");
                    }
                    else
                    {
                        situation.Off();
                        Console.WriteLine("Off");
                    }
                }
            }
        }
    }
}
