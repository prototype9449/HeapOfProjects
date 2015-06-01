using PaladinAndMonsters.Characters;

namespace PaladinAndMonsters.CharacterEntities
{
    public interface IDetermineCharacters
    {
        bool IsRight(Character character1, Character character2);
    }
}