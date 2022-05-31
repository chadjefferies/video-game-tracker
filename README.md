# The Most Awesome Video Game Tracking Api Ever Created

### Getting Started

Before you can run this API locally, you will first need an API Key from [RAWG](https://rawg.io/apidocs).

Store your API key in the local [Secrets Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#secret-manager) in the following format:

```json
{
  "Rawg": {
    "ApiKey": "<KEY GOES HERE>"
  }
}
```

Once your api key is in place, start up the *Example.VideoGameTracker.Api* project and navigate to the `/swagger` page. _You can save this in your launchSettings.json to save time_.

Your first step will be to add users to the service by calling `POST /users`. Make note of the userId returned and add at least two users for the comparison feature later discussed. Please note the users you add are ephemeral and only live for the life of the API. (_Feel free to submit a PR integrating this awesome service with an actual database_) :smile:

Your next step will be to add some games to the users favorites list. You will first need to search for your game(s) by calling `GET /games` and supplying your search criteria. (_Note: Only the first 20 matching games will be returned_). You will need to reference the game ids in your call to `POST /users/:userId/games` to add those games to the user's favorites list.

Once you have your users' favorites all setup, you can now compare one user's favorites list to another user's favorite's list by calling `POST /users/:userId/comparison`. The available comparison methods are:
* `union` - list the favorite games of both users
* `difference` - list the favorite games added by the other user but not by the current user
* `intersection` - list the favorite games that both users have in common

Enjoy!