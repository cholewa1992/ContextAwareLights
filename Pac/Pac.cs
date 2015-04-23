using System;
using System.Collections.Generic;
using System.Linq;
using Pac.Model;


namespace Pac
{
    public class Pac
    {
        private readonly IList<IScenario> _scenarios = new List<IScenario>();

        public void AddSituation(IScenario scenario)
        {
            _scenarios.Add(scenario);
        }

        public void ActOnPeoplePresent(ICollection<Person> people)
        {
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
                if (kvp.Value)
                {
                    kvp.Key.Restore();
                }
                else
                {
                    kvp.Key.Off();
                }
            }
        }
    }
}
