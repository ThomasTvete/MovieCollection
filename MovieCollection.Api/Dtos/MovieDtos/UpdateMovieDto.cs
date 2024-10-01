using System.ComponentModel.DataAnnotations;

namespace MovieCollection.Api.Dtos;
public record class UpdateMovieDto(
    [Required]string Title,
    List<CreateDirectorDto> Directors,
    List<CreateActorDto> Actors,
    List<int> Genres,
    DateOnly ReleaseDate
    );

// public record class UpdateMovieDto(
//     [Required]string Name,
//     string Director,
//     [Required]string Genre,
//     DateOnly ReleaseDate
//     );