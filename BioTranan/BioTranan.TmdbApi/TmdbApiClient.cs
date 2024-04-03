using System.Text.Json;
using RestSharp;

namespace BioTranan.TmdbApi;

public class TmdbApiClient
{
    public async Task<string> GetYouTubeTrailerLinkAsync(string imdbId)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{imdbId}/videos?language=en-US");

        var client = new RestClient(options);

        var request = new RestRequest("");

        request.AddHeader("accept", "application/json");

        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1ZDhhNjlmMGRlNjc5ODVlNzBkNmY0MTc4YmI1MThhMyIsInN1YiI6IjU4Mzk0N2U4YzNhMzY4NDNkZjAwMTc3MiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.Zf0gUivpX2Oba4NwMNTmtYynILEOFvzGcQazA2DNYrY");

        var response = await client.GetAsync(request);

        JsonDocument jsonDocument = JsonDocument.Parse(response.Content);

        JsonElement resultsArray = jsonDocument.RootElement.GetProperty("results");

        string youtubeTrailerKey = string.Empty;

        foreach (JsonElement result in resultsArray.EnumerateArray())
        {
            string name = result.GetProperty("name").GetString();
            string key = result.GetProperty("key").GetString();
            string site = result.GetProperty("site").GetString();

            if (name.ToLower().Contains("trailer") && site.ToLower().Equals("youtube"))
            {
                youtubeTrailerKey = key;
                break;
            }
        }

        return youtubeTrailerKey;
    }
}