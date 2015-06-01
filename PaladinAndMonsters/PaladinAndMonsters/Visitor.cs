using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Locations;
using PaladinAndMonsters.Rules;

namespace PaladinAndMonsters
{
    public class Visitor
    {
        public void Visit(Rule<Paladin, Werewolf> rule, Character character1, Character character2, Location location)
        {
            rule.TryExecute(character1 as Paladin, character2 as Werewolf, location);
        }

        public void Visit(Rule<Werewolf,Paladin> rule, Character character1, Character character2, Location location)
        {
            rule.TryExecute(character1 as Werewolf, character2 as Paladin, location);
        }
    }
}