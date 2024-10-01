namespace MovieCollection.Api.Dtos;

public record class GenreSummaryDto
(
    int Id,
    string Name,
    List<string> Movies
);
