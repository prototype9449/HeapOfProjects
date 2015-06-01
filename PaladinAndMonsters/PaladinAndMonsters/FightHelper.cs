using System;
using System.Collections.Generic;
using PaladinAndMonsters.CharacterEntities;
using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Executors;
using PaladinAndMonsters.Locations;
using PaladinAndMonsters.Rules;

namespace PaladinAndMonsters
{
    public class FightHelper
    {
        public List<NotGenericRule> Rules { get; set; }

        public FightHelper(List<NotGenericRule> rules)
        {
            Rules = new List<NotGenericRule>(rules);
        }

        public void Fight(Character character1, Character character2, Location currentLocation)
        {
            foreach (var rule in Rules)
            {
                rule.ExecuteRule(character1, character2, currentLocation);
            }
        }
    }

}
