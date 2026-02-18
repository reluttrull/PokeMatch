namespace PokeMatch.Model
{
    public class GameLog
    {
        public Enums.GameEvent EventType { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Card>? InvolvedCards { get; set; }
        public string? AdditionalInfo { get; set; } = null;

        public GameLog(Enums.GameEvent eventType) // basic
        {
            EventType = eventType;
            Name = eventType.ToString();
            Timestamp = DateTime.UtcNow;
        }
        public GameLog(Enums.GameEvent eventType, List<Card> involvedCards) // cards involved
        {
            EventType = eventType;
            Name = eventType.ToString();
            InvolvedCards = involvedCards;
            Timestamp = DateTime.UtcNow;
        }
        public GameLog(Enums.GameEvent eventType, PlaySpot playSpot, string? additionalInfo = null) // play spot involved
        {
            EventType = eventType;
            Name = eventType.ToString();
            InvolvedCards = [];
            if (playSpot.MainCard is not null)
            {
                InvolvedCards.Add(playSpot.MainCard);
            }
            InvolvedCards.AddRange(playSpot.AttachedCards);
            Timestamp = DateTime.UtcNow;
            AdditionalInfo = additionalInfo;
        }
    }
}
