using System.Text.Json.Serialization;

namespace Example.VideoGameTracker.Api.Models
{
    public class Game : IEquatable<Game>
    {
        public int Id { get; }
        public string? Name { get; }
        public int? Added { get; }
        public int? Metacritic { get; }
        public decimal? Rating { get; }
        public string? Released { get; }
        public DateTimeOffset? Updated { get; }

        public Game(int id)
        {
            Id = id;
        }

        [JsonConstructor]
        public Game(int id, string? name, int? added, int? metacritic, decimal? rating, string? released, DateTimeOffset? updated)
        {
            Id = id;
            Name = name;
            Added = added;
            Metacritic = metacritic;
            Rating = rating;
            Released = released;
            Updated = updated;
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as Game);
        }
    }
}
