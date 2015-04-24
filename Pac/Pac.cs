using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pac.Model;


namespace Pac
{
    public class Pac
    {
        private readonly IList<IScenario> _scenarios = new List<IScenario>();

        private readonly object _mutex = new object();
        private bool _tick;


        public void AddSituation(IScenario scenario)
        {
            _scenarios.Add(scenario);
            
        }

        public void AutoOffAfter(int seconds)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (_tick)
                        ActOnPeoplePresent(new List<Person>());
                    _tick = true;
                    Thread.Sleep(seconds * 1000);
                }
            });
        }


        public void ActOnPeoplePresent(ICollection<Person> people)
        {
            lock (_mutex) _tick = false;
            var state = new Dictionary<IScenario, bool>();
            foreach (var scenario in _scenarios)
            {
                state[scenario] = false;

                foreach (var person in people)
                {
                    if (scenario.Zone.InZone(person)) state[scenario] = true;
                }
            }
            foreach (var kvp in state)
            {
                if (kvp.Value) kvp.Key.Restore();
                else kvp.Key.Off();
            }
        }
    }
}
