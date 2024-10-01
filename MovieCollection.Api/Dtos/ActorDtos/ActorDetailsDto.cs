namespace MovieCollection.Api.Dtos;

public record class ActorDetailsDto
(
    int Id,
    string Name,
    List<int> Movies

);
