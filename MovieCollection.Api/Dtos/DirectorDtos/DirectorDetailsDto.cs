namespace MovieCollection.Api.Dtos;

public record class DirectorDetailsDto
(
    int Id,
    string Name,
    List<int> Movies
);
