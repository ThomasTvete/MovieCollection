using System.ComponentModel.DataAnnotations;
using MovieCollection.Api.Helpers;

namespace MovieCollection.Api.Dtos;

public record class CreateDirectorDto(
    int? Id, //null for ny entry
    [RequiredIfIdNull(nameof(Id))] string? Name //nullable hvis Id har verdi, Required om ikke

);
