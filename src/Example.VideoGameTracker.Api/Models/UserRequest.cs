namespace Example.VideoGameTracker.Api.Models
{
    public record UserRequest
    {
        public UserRequest(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
