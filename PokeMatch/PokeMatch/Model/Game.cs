namespace PokeMatch.Model
{
    public class GameState
    {
        public Guid Id { get; init; }
        public PlayerState Player1 { get; set; }
        public PlayerState Player2 { get; set; }
        public Card? Stadium { get; set; }
        public GameRecord GameRecord { get; set; } = new GameRecord();

        public GameState(Player player1, Player player2, Deck deck1, Deck deck2)
        {
            Player1 = new(player1, deck1);
            Player2 = new(player2, deck2);
        }

    }
}
