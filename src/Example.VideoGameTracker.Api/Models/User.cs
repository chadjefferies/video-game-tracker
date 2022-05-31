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
        {
            UserId = Interlocked.Increment(ref _globalIdentityUserId);
            FirstName = userRequest.FirstName;
            LastName = userRequest.LastName;
            Games = new FavoriteGameCollection(Enumerable.Empty<Game>());
        }
    }
}
