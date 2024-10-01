namespace MovieCollection.Api.Dtos;


//flere genres??
public record class MovieSummaryDto(
    int Id,
    string Title,
    List<string> Directors,
    List<string> Actors,
    List<string> Genres,
    DateOnly ReleaseDate
    );


// public record class MovieDto(
//     int Id,
//     string Name,
//     string Director,
//     string Genre,
//     DateOnly ReleaseDate
//     );