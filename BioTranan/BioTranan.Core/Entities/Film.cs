namespace BioTranan.Core.Entities;

public class Film
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Year { get; set; }
    public string? Rated { get; set; }
    public string? Released { get; set; }
    public required int RuntimeMinutes { get; set; }
    public string? Genre { get; set; }
    public string? Director { get; set; }
    public string? Writer { get; set; }
    public string? Actors { get; set; }
    public string? Plot { get; set; }
    public string? Language { get; set; }
    public string? Country { get; set; }
    public string? Awards { get; set; }
    public string? Poster { get; set; }
    public string? Metascore { get; set; }
    public string? ImdbRating { get; set; }
    public string? ImdbVotes { get; set; }
    public required string ImdbId { get; set; }
    public string? Type { get; set; }
    public string? DVD { get; set; }
    public string? BoxOffice { get; set; }
    public string? Production { get; set; }
    public string? Website { get; set; }
    public string? Response { get; set; }
    public required uint ScreeningsAllowed { get; set; }
    public bool HasScreeningsRemaining
    {
        get
        {
            if (ScreeningsAllowed > 0)
                return true;
            else
                return false;
        }
    }

    public void DecrementScreeningsAllowed()
    {
        if (ScreeningsAllowed > 0)
        {
            ScreeningsAllowed--;
        }
    }
}
