using System;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;

namespace MovieCollection.Api.Mapping;

public static class GenreMapping
{
    public static GenreDetailsDto ToDetailsDto(this Genre genre)
    {
        return new(
            genre.Id,
            genre.Name,
            genre.Movies.Select(m => m.Id).ToList()
        );
    }
    public static GenreSummaryDto ToSummaryDto(this Genre genre)
    {
        return new(
            genre.Id,
            genre.Name,
            genre.Movies.Select(m => m.Title).ToList()
        );
    }

}
