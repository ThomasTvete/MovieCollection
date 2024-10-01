using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Api.Data;
using MovieCollection.Api.Entities;
using MovieCollection.Api.Mapping;

namespace MovieCollection.Api.Endpoints;

public static class ActorsEndpoints
{
    // PS vurdere å legge til post/put/delete
    // PPS egentlig bare glem det, delete og post er unødvendig, det handler om filmene, men Put er lurt
    public static RouteGroupBuilder MapActorsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("actors");

        group.MapGet("/", async (MovieCollectionContext dbContext) => 
            await dbContext.Actors
                            .AsNoTracking()
                            .Include(a => a.Movies)
                            .Select(a => a.ToSummaryDto())
                            .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, MovieCollectionContext dbContext) =>
        {
            Actor? actor = await dbContext.Actors
                                            .Include(a => a.Movies)
                                            .FirstOrDefaultAsync(a => a.Id == id);
            
            return actor is null ? Results.NotFound() : Results.Ok(actor.ToDetailsDto());
        });

        return group;
    }

}
