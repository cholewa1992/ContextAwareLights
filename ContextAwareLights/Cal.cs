using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContextAwareLights.Model;

namespace ContextAwareLights
{
    public class Cal
    {
        private readonly IList<IScenario> _scenarios = new List<IScenario>();

        private readonly object _mutex = new object();
        private bool _tick;


        public void AddScenario(IScenario scenario)
        {
            _scenarios.Add(scenario);
        }

        public void RemoveScenario(IScenario scenario)
        {
            _scenarios.Remove(scenario);
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

        private static double Distance(Zone zone, Person person)
        {
            if (person.Beacons == null) return 100d;
            var set = person.Beacons.Intersect(zone.Include).ToList();
            var distance = set.Sum(beacon => beacon.Distance);
            return distance / set.Count();
        }

        public void ActOnPeoplePresent(ICollection<Person> people)
        {
            lock (_mutex) _tick = false;
            var state = new Dictionary<IDevice, Tuple<double, bool>>();


            foreach (var device in _scenarios.SelectMany(scenario => scenario.Devices))
            {
                state[device] = new Tuple<double, bool>(-1, false);
            }

            foreach (var person in people)
            {
                foreach (var scenario in _scenarios)
                {

                    if (scenario.Zone.InZone(person))
                    {

                        var priority = scenario.Priority*1000 + (100 - Distance(scenario.Zone, person));

                        foreach (var device in scenario.Devices)
                        {
                            if (state[device].Item1 < priority)
                            {
                                state.Remove(device);
                                state[device] = new Tuple<double, bool>(priority, true);
                            }
                        }
                    }
                }
            }

            foreach (var kvp in state)
            {
                if (kvp.Value.Item2) kvp.Key.Restore();
                else kvp.Key.Off();
            }
        }
    }
}
