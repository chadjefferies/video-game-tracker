using System.ComponentModel.DataAnnotations;
using Example.VideoGameTracker.Api.Validation;

namespace Example.VideoGameTracker.Api.Models
{
    public class GetGamesParameters
    {
        [Required]
        public string? q { get; set; }
        [ValidSortOptions]
        public string? sort { get; set; }
    }
}
