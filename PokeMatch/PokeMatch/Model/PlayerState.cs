namespace PokeMatch.Model
{
    public class PlayerState
    {
        public Player Player { get; set; } = new();
        public Deck Deck { get; set; } = new();
        public List<Card> PrizeCards { get; set; } = [];
        public List<Card> Hand { get; set; } = [];
        public PlaySpot Active { get; set; } = new();
        public List<PlaySpot> Bench { get; set; } = [];
        public List<Card> DiscardPile { get; set; } = [];
        public List<List<Card>> MulliganHands { get; set; } = [];
        public int Mulligans { get; set; }
        public PlayerState(Player player, Deck deck)
        {
            Player = player;
            Deck = deck;
        }

    }
}
