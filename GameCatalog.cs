public class GameCatalog
{
    private List<Game> _games = new List<Game>();

    public string Name { get; private set; }

    public int GamesCount
    {
        get { return _games.Count; }
    }

    public GameCatalog(string name)
    {
        Name = name;
    }

    public bool AddGame(Game game)
    {
        if (FindGameByTitle(game.Title) != null)
        {
            return false;
        }

        _games.Add(game);
        return true;
    }

    public Game? FindGameByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return null;
        }

        string searchTitle = title.Trim();

        foreach (Game game in _games)
        {
            if (string.Equals(game.Title, searchTitle, StringComparison.OrdinalIgnoreCase))
            {
                return game;
            }
        }

        return null;
    }

    public bool RemoveGame(string title)
    {
        Game? game = FindGameByTitle(title);
        if (game == null)
        {
            return false;
        }

        return _games.Remove(game);
    }

    public List<Game> GetGames()
    {
        List<Game> result = new List<Game>();
        foreach (Game game in _games)
        {
            result.Add(game);
        }
        return result;
    }

    public int CountHighRated(double minimumRating)
    {
        int count = 0;
        foreach (Game game in _games)
        {
            if (game.Rating >= minimumRating)
            {
                count += 1;
            }
        }
        return count;
    }
}