namespace Example.VideoGameTracker.Api.Models
{
    public record User
    {
        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public FavoriteGameCollection Games { get; }

        public User(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Games = new FavoriteGameCollection();
        }
    }
}
