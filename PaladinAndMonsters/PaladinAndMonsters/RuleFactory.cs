using System.Collections.Generic;
using PaladinAndMonsters.CharacterEntities;
using PaladinAndMonsters.Executors;
using PaladinAndMonsters.Locations;
using PaladinAndMonsters.Rules;

namespace PaladinAndMonsters
{
    public class RuleFactory
    {
        public List<NotGenericRule> GetAllRules()
        {
            var rule1 = new Rule1(new PaladinAndWerewolf(), new WeirdLocation(),
                new OtherExecutionRule());

            var rule2 = new Rule2(new PaladinAndWerewolf(), new WeirdLocation(),
                new WeakHealthWereWolfPaladinExecutionRule());

            return new List<NotGenericRule> { rule1, rule2 };
        }
    }
}