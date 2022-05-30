namespace Example.VideoGameTracker.Api.Models
{
    public record UserRequest
    {
        public UserRequest(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
}
