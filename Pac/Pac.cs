using System;
using System.Collections.Generic;
using Pac.Model;


namespace Pac
{
    class Pac
    {
        private readonly IList<IScenario> _scenarios = new List<IScenario>();
        
        public void AddSituation(IScenario scenario)
        {
            _scenarios.Add(scenario);
        }

        public void ActOnPeoplePresent(ICollection<Person> people)
        {
			var _state = new Dictionary<IScenario, bool> ();
            foreach (var person in people)
            {
                foreach (var scenario in _scenarios)
                {
                    if (scenario.Zone.InZone(person))
                    {
						_state [scenario] = _state [scenario] || true;
                    }
                }
            }
			foreach (var kvp in _state) {
				if (kvp.Value) {
					kvp.Key.Restore ();
				} else {
					kvp.Key.Off ();
				}
			}
        }
    }
}
