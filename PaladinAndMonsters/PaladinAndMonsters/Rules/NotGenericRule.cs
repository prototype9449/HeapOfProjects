using PaladinAndMonsters.Characters;
using PaladinAndMonsters.Locations;

namespace PaladinAndMonsters.Rules
{
    public abstract class NotGenericRule
    {
        public Visitor RuleController = new Visitor();

        protected NotGenericRule(Visitor visitor)
        {
            RuleController = visitor;
        }

        protected NotGenericRule()
        {
            
        }

        public abstract void ExecuteRule(Character character1, Character character2, Location location);

    }
}