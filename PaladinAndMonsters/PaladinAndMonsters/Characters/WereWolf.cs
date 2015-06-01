namespace PaladinAndMonsters.Characters
{
    public class Werewolf : Character
    {
        public int Health { get; set; }
        public int Stamina { get; set; }
        public int WolfPower { get; set; }

        public Werewolf( int health, int stamina, int stregth)
        {
            Health = health;
            Stamina = stamina;
            WolfPower = stregth;
        }
    }
}