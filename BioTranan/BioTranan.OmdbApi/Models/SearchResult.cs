namespace BioTranan.OmdbApi.Models;

public class SearchResult
{
    public List<OmdbMovie> Search { get; set; }
    public string totalResults { get; set; }
    public string Response { get; set; }
}
