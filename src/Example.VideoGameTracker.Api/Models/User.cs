namespace Example.VideoGameTracker.Api.Models
{
    public record User 
    {
        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public FavoriteGames Games { get; }

        public User(int userId, string firstName, string lastName, IEnumerable<Game> games)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Games = new FavoriteGames(games);
        }

        public User(UserRequest userRequest)
        {
            UserId = userRequest.UserId;
            FirstName = userRequest.FirstName;
            LastName = userRequest.LastName;
            Games = new FavoriteGames(Enumerable.Empty<Game>());
        }
    }
}
