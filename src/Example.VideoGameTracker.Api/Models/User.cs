namespace Example.VideoGameTracker.Api.Models
{
    public record User 
    {
        public User(int userId, string firstName, string lastName, string email, IEnumerable<Game> games)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            FavoriteGames = new FavoriteGames(games);
        }

        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public FavoriteGames FavoriteGames { get; }
    }
}
