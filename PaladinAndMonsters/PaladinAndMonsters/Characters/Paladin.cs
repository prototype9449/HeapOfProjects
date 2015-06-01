namespace PaladinAndMonsters.Characters
{
    public class Paladin : Character
    {
        public int Health { get; set; }
        public int MagicPower { get; set; }

        public Paladin(int health, int magicPower)
        {
            Health = health;
            MagicPower = magicPower;
        }
    }
}