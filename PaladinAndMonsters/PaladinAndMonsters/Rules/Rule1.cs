using PaladinAndMonsters.CharacterEntities;
using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Executors;
using PaladinAndMonsters.Locations;

namespace PaladinAndMonsters.Rules
{
    public class Rule1 : Rule<Paladin, Werewolf>
    {
        public Rule1(IDetermineCharacters determineCharacters, IDetermineLocation determineLocation, IExecutionRule<Paladin, Werewolf> executionRule) :
            base(determineCharacters, determineLocation, executionRule)
        {

        }

        public override void ExecuteRule(Character character1, Character character2, Location location)
        {
            RuleController.Visit(this, character1, character2, location);
        }
    }
}