using System;
using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Locations;

namespace PaladinAndMonsters
{
    class Program
    {
        static void Main(string[] args)
        {
            var ruleFactory = new RuleFactory();
            var fightHelper = new FightHelper(ruleFactory.GetAllRules());
            fightHelper.Fight(new Paladin(10,400), new Werewolf(1, 100, 123), new Church());
        }
    }
}
