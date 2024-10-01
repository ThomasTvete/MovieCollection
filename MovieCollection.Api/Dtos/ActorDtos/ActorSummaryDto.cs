namespace MovieCollection.Api.Dtos;

public record class ActorSummaryDto
(
    int Id,
    string Name,
    List<string> Movies
);
