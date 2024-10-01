namespace MovieCollection.Api.Dtos;

public record class GenreDetailsDto
(
    int Id,
    string Name,
    List<int> Movies

);