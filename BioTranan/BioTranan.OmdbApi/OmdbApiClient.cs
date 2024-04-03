using System.Net.Http.Json;
using BioTranan.OmdbApi.Models;

namespace BioTranan.OmdbApi;

public class OmdbApiClient
{
    private Uri _baseAddress;
    private readonly string _apiKey;
    private HttpClient _httpClient;

    public OmdbApiClient(string apiKey) : this(apiKey, new Uri("http://www.omdbapi.com"))
    {
    }

    public OmdbApiClient(string apiKey, Uri baseAddress)
    {
        this._baseAddress = baseAddress;
        this._apiKey = apiKey;
        this._httpClient = new HttpClient();
        this._httpClient.BaseAddress = _baseAddress;
    }

    public async Task<OmdbMovie> GetMovieByTitleAsync(string movieTitle)
    {
        movieTitle = movieTitle.Trim().Replace(" ", "+");

        var parameters = new Dictionary<string, string>
        {
            {"t", movieTitle.Trim().Replace(" ", "+")},
            {"plot", "full"},
            {"apikey", _apiKey},
            {"type", "movie"}
        };

        string apiUrl = ConstructApiUrl(parameters);

        OmdbMovie omdbMovie = await _httpClient.GetFromJsonAsync<OmdbMovie>(apiUrl);

        return omdbMovie;
    }

    public async Task<SearchResult> SearchMovieByTitleAsync(string movieTitle)
    {
        movieTitle = movieTitle.Trim().Replace(" ", "+");

        var parameters = new Dictionary<string, string>
            {
                {"s", movieTitle.Trim().Replace(" ", "+")},
                {"plot", "full"},
                {"apikey", _apiKey},
                {"type", "movie"}
            };

        string apiUrl = ConstructApiUrl(parameters);

        SearchResult searchResult = await _httpClient.GetFromJsonAsync<SearchResult>(apiUrl);

        return searchResult;
    }

    public async Task<OmdbMovie> GetMovieByImdbIdAsync(string imdbId)
    {
        var parameters = new Dictionary<string, string>
            {
                {"i", imdbId},
                {"plot", "full"},
                {"apikey", _apiKey},
                {"type", "movie"}
            };

        string apiUrl = ConstructApiUrl(parameters);

        OmdbMovie omdbMovie = await _httpClient.GetFromJsonAsync<OmdbMovie>(apiUrl);

        return omdbMovie;
    }

    private string ConstructApiUrl(Dictionary<string, string> parameters)
    {
        var queryString = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
        return $"{_baseAddress}?{queryString}";
    }
}