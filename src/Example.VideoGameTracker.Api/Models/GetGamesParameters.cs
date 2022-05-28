using Example.VideoGameTracker.Api.Validation;

namespace Example.VideoGameTracker.Api.Models
{
    public class GetGamesParameters
    {
        public string q { get; set; }
        [ValidValues(null,
            "name", "-name",
            "released", "-released",
            "added", "-added",
            "created", "-created",
            "updated", "-updated",
            "rating", "-rating",
            "metacritic", "-metacritic")]
        public string? sort { get; set; }
    }
}
