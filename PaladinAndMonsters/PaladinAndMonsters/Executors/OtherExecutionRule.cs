using PaladinAndMonsters.Characters;

namespace PaladinAndMonsters.Executors
{
    public class OtherExecutionRule : IExecutionRule<Paladin, Werewolf>
    {
        public void ExecuteFor(Paladin paladin, Werewolf wereWolf)
        {
            wereWolf.Health -= 10;
            wereWolf.WolfPower -= 100;

            paladin.Health += 50;
            paladin.MagicPower += 100;
        }
    }
}