using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Api.Data;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;
using MovieCollection.Api.Helpers;
using MovieCollection.Api.Mapping;

namespace MovieCollection.Api.Endpoints;

public static class MoviesEndpoints
{
    const string GetMovieEndpoint = "GetMovie";

    //flere genres
    // private static readonly List<MovieSummaryDto> movies = [
    // new(
    //     1,
    //     "The Lighthouse",
    //     "Robert Eggers",
    //     new List<string> { "Horror", "Comedy", "Expressionism" },
    //     new DateOnly(2019, 10, 18)
    // ),
    // new(
    //     2,
    //     "The Thing",
    //     "John Carpenter",
    //     new List<string> { "Horror", "Sci-fi" },
    //     new DateOnly(1982, 6, 25)
    // ),
    // new(
    //     3,
    //     "The Holy Mountain",
    //     "Alejandro Jodorowsky",
    //     new List <string> { "Drama", "Adventure", "Surrealist" },
    //     new DateOnly(1973, 11, 29)
    // )
    // ];

    // private static readonly List<MovieDto> movies = [
    // new(
    //     1,
    //     "The Lighthouse",
    //     "Robert Eggers",
    //     "Buddy-film",
    //     new DateOnly(2019, 10, 18)
    // ),
    // new(
    //     2,
    //     "The Thing",
    //     "John Carpenter",
    //     "Horror",
    //     new DateOnly(1982, 6, 25)
    // ),
    // new(
    //     3,
    //     "The Holy Mountain",
    //     "Alejandro Jodorowsky",
    //     "Surrealist",
    //     new DateOnly(1973, 11, 29)
    // )
    // ];

    public static RouteGroupBuilder MapMoviesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("movies")
                        .WithParameterValidation();

        //skriver "/movies" i url, returnerer movies liste

        // group.MapGet("/", () => movies);

        group.MapGet("/", async (MovieCollectionContext dbContext) =>
            await dbContext.Movies
                            .AsNoTracking()
                            .Include(m => m.Directors)
                            .Include(m => m.Actors)
                            .Include(m => m.Genres)
                            .Select(m => m.ToSummaryDto())
                            .ToListAsync()
        );

        //GET for film per id
        group.MapGet("/{id}", async (int id, MovieCollectionContext dbContext) =>
            {
                // var movie = movies.Find(movie => movie.Id == id);

                Movie? movie = await dbContext.Movies
                                    .Include(m => m.Directors)
                                    .Include(m => m.Actors)
                                    .Include(m => m.Genres)
                                    .FirstOrDefaultAsync(m => m.Id == id);

                return movie is null ? Results.NotFound() : Results.Ok(movie.ToDetailsDto());
            })
        .WithName(GetMovieEndpoint);

        //POST lage ny entry for film (og tilhørende regissør/skuespillere)
        _ = group.MapPost("/", async (CreateMovieDto newMovie, MovieCollectionContext dbContext) =>
        {
            // MovieDto movie = new(
            //     movies.Count + 1,
            //     newMovie.Title,
            //     newMovie.Director,
            //     newMovie.Genres,
            //     newMovie.ReleaseDate);

            // Movie movie = new()
            // {
            //     Title = newMovie.Title,
            //     Director = newMovie.Director,
            //     Genres = new List<Genre>(),
            //     ReleaseDate = newMovie.ReleaseDate
            // };


            Movie movie = await newMovie.ToEntityAsync(dbContext);

            // foreach (var genreName in newMovie.Genres)
            // {
            //     Genre? existingGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            //     if (existingGenre != null)
            //     {
            //         movie.Genres.Add(existingGenre);
            //     }

            // }

            dbContext.Movies.Add(movie);
            await dbContext.SaveChangesAsync();

            // MovieDto movieDto = new(
            //     movie.Id,
            //     movie.Title,
            //     movie.Director,
            //     movie.Genres.Select(g => g.Name).ToList(),
            //     movie.ReleaseDate
            // );

            return Results.CreatedAtRoute(GetMovieEndpoint, new { id = movie.Id }, movie.ToDetailsDto());
        });

        // PUT update film per id
        group.MapPut("/{id}", async (int id, UpdateMovieDto updatedMovie, MovieCollectionContext dbContext) =>
        {
            Movie? existingMovie = await dbContext.Movies
                                    .Include(m => m.Directors)
                                    .Include(m => m.Actors)
                                    .Include(m => m.Genres)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie is null)
            {
                return Results.NotFound();
            }

            await MovieEndpointHelpers.CustomSetValuesAsync(existingMovie, updatedMovie, dbContext);

            await dbContext.SaveChangesAsync();

            // De neste 12 linjene funker ikke fordi many-to-many relationships(?)
            // Movie movie = updatedMovie.ToEntity(id);

            // foreach (var genreName in updatedMovie.Genres)
            // {
            //     Genre? existingGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            //     if (existingGenre != null)
            //     {
            //         movie.Genres.Add(existingGenre);
            //     }

            // }

            // dbContext.Entry(existingMovie).CurrentValues.SetValues(movie);

            // movies[index] = new MovieSummaryDto(
            //     id,
            //     updatedMovie.Title,
            //     updatedMovie.Director,
            //     updatedMovie.Genres,
            //     updatedMovie.ReleaseDate
            // );

            return Results.NoContent();
        });

        //DELETE slette film per id
        group.MapDelete("/{id}", async (int id, MovieCollectionContext dbContext) =>
        {
            // movies.RemoveAll(movie => movie.Id == id);

            // Movie? movie = await dbContext.Movies.FindAsync(id);
            // if(movie is null) return Results.NotFound();

            // dbContext.Movies.Remove(movie);
            // await dbContext.SaveChangesAsync();

            await dbContext.Movies
                    .Where(m => m.Id == id)
                    .ExecuteDeleteAsync();

            

            return Results.NoContent();
        });

        return group;

    }

}
