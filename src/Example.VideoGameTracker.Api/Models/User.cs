namespace Example.VideoGameTracker.Api.Models
{
    public record User
    {
        private static int _globalIdentityUserId = 0;

        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public FavoriteGameCollection Games { get; }

        public User(UserRequest userRequest)
            : this(userRequest.FirstName, userRequest.LastName)
        { }

        public User(string firstName, string lastName)
        {
            UserId = Interlocked.Increment(ref _globalIdentityUserId);
            FirstName = firstName;
            LastName = lastName;
            Games = new FavoriteGameCollection();
        }
    }
}
