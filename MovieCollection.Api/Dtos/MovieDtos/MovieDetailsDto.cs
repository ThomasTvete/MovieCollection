namespace MovieCollection.Api.Dtos;


//flere genres??
public record class MovieDetailsDto(
    int Id,
    string Title,
    List<int> Directors,
    List<int> Actors,
    List<int> Genres,
    DateOnly ReleaseDate
    );


// public record class MovieDto(
//     int Id,
//     string Name,
//     string Director,
//     string Genre,
//     DateOnly ReleaseDate
//     );