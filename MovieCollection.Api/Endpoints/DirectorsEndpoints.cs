using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Api.Data;
using MovieCollection.Api.Entities;
using MovieCollection.Api.Mapping;

namespace MovieCollection.Api.Endpoints;

public static class DirectorsEndpoints
{
    public static RouteGroupBuilder MapDirectorsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("directors");

        group.MapGet("/", async (MovieCollectionContext dbContext) => 
            await dbContext.Directors
                            .AsNoTracking()
                            .Include(d => d.Movies)
                            .Select(d => d.ToSummaryDto())
                            .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, MovieCollectionContext dbContext) =>
        {
            Director? director = await dbContext.Directors
                                            .Include(d => d.Movies)
                                            .FirstOrDefaultAsync(d => d.Id == id);
            
            return director is null ? Results.NotFound() : Results.Ok(director.ToDetailsDto());
        });

        return group;
    }

}
