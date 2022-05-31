using System.ComponentModel.DataAnnotations;

namespace Example.VideoGameTracker.Api.Validation
{
    public class ValidSortOptionsAttribute : ValidationAttribute
    {
        static string[] _args = new string[] { 
            "name", "-name",
            "released", "-released",
            "added", "-added",
            "created", "-created",
            "updated", "-updated",
            "rating", "-rating",
            "metacritic", "-metacritic" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || _args.Contains((string)value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"{value} is not a valid sort option. Please use one of the following: {string.Join(", ", _args)}.");
        }
    }
}
