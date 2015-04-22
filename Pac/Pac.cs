using System;
using System.Collections.Generic;
using System.Linq;
using Ocon;
using Ocon.TcpCom;
using Ocon.OconSerializer;
using Ocon.OconCommunication;
using Pac.Model;
using ubilight;
using ubilight.LightingSystems;
using Zone = Pac.Model.Zone;


namespace Pac
{
    class Pac
    {
        private readonly IList<ISituation> _situations = new List<ISituation>();
        

        public Pac()
        {
            /*
            var comHelper = new OconComHelper(new TcpCom(new JsonNetAdapter()));
            var client = new OconClient(comHelper);
            client.Subscribe(new Situation<ComparableCollection<Person>>(c => new ComparableCollection<Person>(c.OfType<Person>())));
            client.SituationStateChangedEvent += situation => ActOnPeoplePresent( ((Situation<ComparableCollection<Person>>) situation).Value );

            var central = new OconCentral(new OconContextFilter(), comHelper);
            comHelper.Broadcast(DeviceType.Central, 5);

            comHelper.DiscoveryEvent += peer => Console.WriteLine("Found peer {0}", peer);

            
            var zone = new Zone(new Beacon {Major = 5000, Minor = 3927, Proximity = Proximity.Near});

            var ubilight = new Ubilight(new List<ILightingSystem>() { new HueLightingSystem("hue") });

            AddSituation(new Situation(zone, new[]{"hue1", "hue2"}, ubilight));
             */
        }

        public void AddSituation(ISituation situation)
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
                        situation.On();
                    }
                    else
                    {
                        situation.Off();
                    }
                }
            }
        }
    }
}
