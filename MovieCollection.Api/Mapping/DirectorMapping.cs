using System;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;

namespace MovieCollection.Api.Endpoints;

public static class DirectorMapping
{
    public static DirectorDetailsDto ToDetailsDto(this Director director)
    {
        return new(
            director.Id,
            director.Name,
            director.Movies.Select(m => m.Id).ToList()
        );
    }
    public static DirectorSummaryDto ToSummaryDto(this Director director)
    {
        return new(
            director.Id,
            director.Name,
            director.Movies.Select(m => m.Title).ToList()
        );
    }

}
