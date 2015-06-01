using PaladinAndMonsters.CharacterEntities;
using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Executors;
using PaladinAndMonsters.Locations;

namespace PaladinAndMonsters.Rules
{
    public class Rule2 : Rule<Werewolf, Paladin>
    {
        public Rule2(IDetermineCharacters determineCharacters, IDetermineLocation determineLocation, IExecutionRule<Werewolf, Paladin> executionRule)
            : base(determineCharacters, determineLocation, executionRule)
        {
        }

        public override void ExecuteRule(Character character1, Character character2, Location location)
        {
            RuleController.Visit(this, character1, character2, location);
        }
    }
}