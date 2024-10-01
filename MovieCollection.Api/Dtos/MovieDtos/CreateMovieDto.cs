using System.ComponentModel.DataAnnotations;

namespace MovieCollection.Api.Dtos;

// flere genres
public record class CreateMovieDto(
    [Required]string Title,
    List<CreateDirectorDto> Directors, //må ta hele objektet for fleksibilitet (ny eller eksisterende instans)
    List<CreateActorDto> Actors, //samme som over 
    List<int> Genres, //klienten får ikke love til å lage egen instans av genre, så Id til eksisterende genre er alt som trengs
    DateOnly ReleaseDate
    );

// public record class CreateMovieDto(
//     [Required]string Name,
//     string Director,
//     [Required]string Genre,
//     DateOnly ReleaseDate
//     );