public class ReviewBoard {
    private readonly GameCatalog _catalog;
    private readonly List<ReviewSession> _sessions = new List<ReviewSession>();

    public ReviewBoard(GameCatalog catalog) {
        _catalog = catalog;
    }

    public bool AddSession(ReviewSession session) {
        if (session.Status != ReviewStatus.Open) {
            return false;
        }
        if (FindSession(session.Id) != null) {
            return false;
        }

        _sessions.Add(session);
        return true;
    }

    public ReviewSession? FindSession(int sessionId) {
        foreach (ReviewSession session in _sessions) {
            if (session.Id == sessionId) {
                return session;
            }
        }
        return null;
    }

    public List<ReviewSession> GetSessions() {
        return new List<ReviewSession>(_sessions);
    }

    public bool IsUnderReview(Game game) {
        foreach (ReviewSession session in _sessions) {
            if (session.Status == ReviewStatus.InProgress && session.TargetGame == game) {
                return true;
            }
        }
        return false;
    }

    public bool TryStart(int sessionId, string gameTitle) {
        ReviewSession? session = FindSession(sessionId);
        if (session == null) {
            return false;
        }

        Game? game = _catalog.FindGameByTitle(gameTitle);
        if (game == null) {
            return false;
        }

        if (IsUnderReview(game)) {
            return false;
        }

        return session.TryStart(game);
    }

    public bool TryFinish(int sessionId) {
        ReviewSession? session = FindSession(sessionId);
        if (session == null) {
            return false;
        }
        return session.TryFinish();
    }

    public bool TryCancel(int sessionId) {
        ReviewSession? session = FindSession(sessionId);
        if (session == null) {
            return false;
        }
        return session.TryCancel();
    }

    public bool TryRemoveGame(string gameTitle) {
        Game? game = _catalog.FindGameByTitle(gameTitle);
        if (game == null) {
            return false;
        }

        foreach (ReviewSession session in _sessions) {
            if (session.TargetGame == game) {
                return false;
            }
        }

        return _catalog.RemoveGame(game.Title);
    }

    public int CountByStatus(ReviewStatus status) {
        int count = 0;
        foreach (ReviewSession session in _sessions) {
            if (session.Status == status) {
                count++;
            }
        }
        return count;
    }
}
