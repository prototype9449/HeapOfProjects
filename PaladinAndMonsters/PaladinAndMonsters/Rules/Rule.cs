using PaladinAndMonsters.CharacterEntities;
using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Executors;
using PaladinAndMonsters.Locations;

namespace PaladinAndMonsters.Rules
{
    public abstract class Rule<T, U> : NotGenericRule
        where T : Character
        where U : Character
    {
        public IDetermineCharacters DetermineCharacters { get; set; }
        public IDetermineLocation DetermineLocation { get; set; }
        public IExecutionRule<T, U> ExecutionRule { get; set; }

        protected Rule(IDetermineCharacters determineCharacters, IDetermineLocation determineLocation,
            IExecutionRule<T, U> executionRule) : base()
        {
            DetermineCharacters = determineCharacters;
            DetermineLocation = determineLocation;
            ExecutionRule = executionRule;
        }

        public void TryExecute(T character1, U character2, Location location)
        {
            if(DetermineCharacters.IsRight(character1, character2) && DetermineLocation.IsRight(location))
                ExecutionRule.ExecuteFor(character1, character2);
        }
    }

    
}