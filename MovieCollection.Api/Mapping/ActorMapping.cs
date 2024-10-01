using System;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;

namespace MovieCollection.Api.Endpoints;

public static class ActorMapping
{
    public static ActorDetailsDto ToDetailsDto(this Actor actor)
    {
        return new(
            actor.Id,
            actor.Name,
            actor.Movies.Select(m => m.Id).ToList()
        );
    }
    public static ActorSummaryDto ToSummaryDto(this Actor actor)
    {
        return new(
            actor.Id,
            actor.Name,
            actor.Movies.Select(m => m.Title).ToList()
        );
    }

}