namespace PaladinAndMonsters.Locations
{
    public class WeirdLocation : IDetermineLocation
    {
        public bool IsRight(Location location)
        {
            return location is Church || location is CursePlace;
        }
    }
}