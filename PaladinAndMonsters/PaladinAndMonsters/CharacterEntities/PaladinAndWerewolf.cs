using PaladinAndMonsters.Characters;

namespace PaladinAndMonsters.CharacterEntities
{
    public class PaladinAndWerewolf : IDetermineCharacters
    {
        public bool IsRight(Character character1, Character character2)
        {
            return (character1 is Paladin && character2 is Werewolf) ||
                   (character1 is Werewolf && character2 is Paladin);
        }
    }
}