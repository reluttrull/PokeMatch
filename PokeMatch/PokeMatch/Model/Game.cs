namespace PokeMatch.Model
{
    public class GameState(string joinCode, Player player1, Player player2, Deck deck1, Deck deck2)
    {
        public Guid Id { get; init; }
        public string JoinCode { get; init; } = joinCode;
        public PlayerState Player1 { get; set; } = new(player1, deck1);
        public PlayerState Player2 { get; set; } = new(player2, deck2);
        public Card? Stadium { get; set; }
        public GameRecord GameRecord { get; set; } = new GameRecord();
    }
}
