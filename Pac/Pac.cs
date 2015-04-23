using System;
using System.Collections.Generic;
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
            foreach (var scenario in _scenarios)
            {
                if (people.Count == 0) scenario.Off();

                foreach (var person in people)
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
