public class ReviewSession {
    public int Id { get; private set; }
    public int MinReleaseYear { get; private set; }
    public double ProposedRating { get; private set; }
    public Game? TargetGame { get; private set; }
    public ReviewStatus Status { get; private set; }

    public ReviewSession(int id, int minReleaseYear, double proposedRating) {
        Id = id;
        MinReleaseYear = minReleaseYear < 1950 ? 1950 : minReleaseYear;

        if (proposedRating < 0.0) ProposedRating = 0.0;
        else if (proposedRating > 10.0) ProposedRating = 10.0;
        else ProposedRating = proposedRating;

        TargetGame = null;
        Status = ReviewStatus.Open;
    }

    public bool TryStart(Game game) {
        if (Status != ReviewStatus.Open) {
            return false;
        }
        if (game.ReleaseYear < MinReleaseYear) {
            return false;
        }

        TargetGame = game;
        Status = ReviewStatus.InProgress;
        return true;
    }

    public bool TryFinish() {
        if (Status != ReviewStatus.InProgress) {
            return false;
        }

        Game? game = TargetGame;
        if (game == null) {
            return false;
        }
        if (game.ReleaseYear < MinReleaseYear) {
            return false;
        }

        game.UpdateRating(ProposedRating);
        Status = ReviewStatus.Completed;
        return true;
    }

    public bool TryCancel() {
        if (Status != ReviewStatus.Open && Status != ReviewStatus.InProgress) {
            return false;
        }

        Status = ReviewStatus.Cancelled;
        return true;
    }

    public string GetStatusText() {
        switch (Status) {
            case ReviewStatus.Open:
                return "открыта";
            case ReviewStatus.InProgress:
                return "в работе";
            case ReviewStatus.Completed:
                return "завершена";
            case ReviewStatus.Cancelled:
                return "отменена";
            default:
                return "неизвестно";
        }
    }

    public string GetInfo() {
        string gameText = TargetGame != null ? TargetGame.Title : "не выбрана";
        return $"Сессия №{Id} оценка {ProposedRating} {GetStatusText()}, игра {gameText}";
    }
}
