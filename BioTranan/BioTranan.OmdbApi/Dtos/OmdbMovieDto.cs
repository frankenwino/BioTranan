namespace BioTranan.OmdbApi.Dtos;

public record class OmdbMovieDto(
    string Title,
    string Year,
    string Rated,
    string Released,
    string Runtime,
    string Genre,
    string Director,
    string Writer,
    string Actors,
    string Plot,
    string Language,
    string Country,
    string Awards,
    string Poster,
    string Metascore,
    string ImdbRating,
    string ImdbVotes,
    string ImdbID,
    string Type,
    string DVD,
    string BoxOffice,
    string Production,
    string Website,
    string Response
);