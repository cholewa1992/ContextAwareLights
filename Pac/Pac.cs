using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ocon;
using Ocon.Messages;
using Ocon.TcpCom;
using Ocon.OconSerializer;
using Ocon.OconCommunication;
using Pac.Model;


namespace Pac
{
    class Pac
    {
        public Pac()
        {
            var comHelper = new OconComHelper(new TcpCom(new JsonNetAdapter()));
            var client = new OconClient(comHelper);
            client.Subscribe(new Situation<ComparableCollection<Person>>(c => new ComparableCollection<Person>(c.OfType<Person>())));
            client.SituationStateChangedEvent += (situation) => ActOnPeoplePresent( ((Situation<ComparableCollection<Person>>) situation).Value );

            var central = new OconCentral(new OconContextFilter(), comHelper);
            comHelper.Broadcast(DeviceType.Central, 5);
        }


        public void ActOnPeoplePresent(ICollection<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine(person.Id);
                foreach (var beacon in person.Beacons)
                {
                    Console.WriteLine("\t {0}: {1}",beacon.Id, beacon.Proximity);
                }
            }
        }
    }
}
