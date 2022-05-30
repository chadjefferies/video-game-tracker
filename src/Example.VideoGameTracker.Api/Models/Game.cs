namespace Example.VideoGameTracker.Api.Models
{
    public class Game : IEquatable<Game>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Added { get; set; }
        public int? Metacritic { get; set; }
        public decimal? Rating { get; set; }
        public string Released { get; set; }
        public DateTimeOffset? Updated { get; set; }

        public Game(int id)
        {
            Id = id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public bool Equals(Game? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(other, this))
                return true;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Game);
        }
    }
}
