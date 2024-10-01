using System;
using MovieCollection.Api.Data;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;
using MovieCollection.Api.Helpers;

namespace MovieCollection.Api.Mapping;

public static class MovieMapping
{
    public static async Task<Movie> ToEntityAsync(this CreateMovieDto movie, MovieCollectionContext dbContext)
    {

        Movie entity = new()
        {
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate

            // foreach (var genreName in newMovie.Genres)
            // {
            //     Genre? existingGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            //     if (existingGenre != null)
            //     {
            //         movie.Genres.Add(existingGenre);
            //     }

            // }
        };

        await entity.PopulateDirectorsAsync(movie.Directors, dbContext);
        await entity.PopulateActorsAsync(movie.Actors, dbContext);
        await entity.PopulateGenresAsync(movie.Genres, dbContext);

        return entity;
    }

        // Brukes ikke
    // public static async Task<Movie> ToEntityAsync(this UpdateMovieDto movie, int id)
    // {
    //     Movie entity = new()
    //     {
    //         Id = id,
    //         Title = movie.Title,
    //         Directors = new List<Director>(),
    //         Actors = new List<Actor>(),
    //         Genres = new List<Genre>(),
    //         ReleaseDate = movie.ReleaseDate
    //     };
    // }


    //Kanskje lage i alle fall en egen Int liste i Movie Entity for hver foreign tabell, slik at .Select ikke trengs å gjøres mer enn en gang?
    // NB, er gjort: se Movie entity og PopulateDirector/Actor/Genre metodene
    // NVM BARE GLEM DET, idiot, da vil ikke MapGet funke med mindre jeg lagrer alle listene i databasen, fuck that
    public static MovieSummaryDto ToSummaryDto(this Movie movie)
    {
        return new(
        movie.Id,
        movie.Title,
        movie.Directors.Select(d => d.Name).ToList(),
        movie.Actors.Select(a => a.Name).ToList(),
        movie.Genres.Select(g => g.Name).ToList(),
        movie.ReleaseDate
        );
    }
    public static MovieDetailsDto ToDetailsDto(this Movie movie)
    {
        return new(
        movie.Id,
        movie.Title,
        movie.Directors.Select(d => d.Id).ToList(),
        movie.Actors.Select(a => a.Id).ToList(),
        movie.Genres.Select(g => g.Id).ToList(),
        movie.ReleaseDate
        );
    }

}


