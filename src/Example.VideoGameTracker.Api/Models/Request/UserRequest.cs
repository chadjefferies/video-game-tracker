namespace Example.VideoGameTracker.Api.Models.Request
{
    public class UserRequest
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
