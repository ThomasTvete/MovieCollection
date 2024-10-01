namespace MovieCollection.Api.Dtos;

public record class DirectorSummaryDto
(
    int Id,
    string Name,
    List<string> Movies
);