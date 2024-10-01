using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Api.Data;
using MovieCollection.Api.Entities;
using MovieCollection.Api.Mapping;

namespace MovieCollection.Api.Endpoints;

public static class GenresEndpoints
{
    //For å kunne hente ut info fra genres tabellen (husk å gjøre det samme for Actors of Directors)
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (MovieCollectionContext dbContext) => 
            await dbContext.Genres
                            .AsNoTracking()
                            .Include(g => g.Movies)
                            .Select(g => g.ToSummaryDto())
                            .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, MovieCollectionContext dbContext) =>
        {
            Genre? genre = await dbContext.Genres
                                            .Include(g => g.Movies)
                                            .FirstOrDefaultAsync(g => g.Id == id);
            
            return genre is null ? Results.NotFound() : Results.Ok(genre.ToDetailsDto());
        });

        return group;
    }

}
