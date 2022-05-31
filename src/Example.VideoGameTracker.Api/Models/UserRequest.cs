namespace Example.VideoGameTracker.Api.Models
{
    public record UserRequest
    {
        public string FirstName { get; }
        public string LastName { get; }

        public UserRequest(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
