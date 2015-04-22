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
            foreach (var person in people)
            {
                foreach (var scenario in _scenarios)
                {
                    if (scenario.Zone.InZone(person))
                    {
                        scenario.Restore();
                    }
                    else
                    {
                        scenario.Off();
                    }
                }
            }
        }
    }
}
