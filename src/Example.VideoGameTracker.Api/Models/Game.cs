namespace Example.VideoGameTracker.Api.Models
{
    public class Game: IEquatable<Game> 
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public int Added { get; set; }
        public int Metacritic { get; set; }
        public decimal Rating { get; set; }
        public DateTimeOffset Released { get; set; }
        public DateTimeOffset Updated { get; set; }

        public bool Equals(Game? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(other, this))
                return true;

            return GameId == other.GameId;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Game);
        }
    }
}
