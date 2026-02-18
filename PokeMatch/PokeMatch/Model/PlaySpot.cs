namespace PokeMatch.Model
{
    public class PlaySpot
    {
        public Card? MainCard { get; set; }
        public List<Card> AttachedCards { get; set; } = [];
        public int DamageCounters { get; set; } = 0;
    }
}
