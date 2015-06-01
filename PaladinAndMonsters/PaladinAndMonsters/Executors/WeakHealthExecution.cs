using PaladinAndMonsters.Characters;

namespace PaladinAndMonsters.Executors
{
    public class WeakHealthWereWolfPaladinExecutionRule : IExecutionRule<Werewolf, Paladin>
    {
        public void ExecuteFor(Werewolf wereWolf, Paladin paladin)
        {
            wereWolf.Health += 10;
            wereWolf.WolfPower += 100;

            paladin.MagicPower -= 10;
            paladin.Health -= 10;
        }
    }

   
}