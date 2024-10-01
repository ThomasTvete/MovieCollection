using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Api.Data;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;
using MovieCollection.Api.Mapping;

namespace MovieCollection.Api.Helpers;

public static class MovieEndpointHelpers
{

    

    public static async Task CustomSetValuesAsync(Movie movie, UpdateMovieDto updatedMovie, MovieCollectionContext dbContext)
    {
        movie.Directors.Clear();
        movie.Actors.Clear();
        movie.Genres.Clear();

        movie.Title = updatedMovie.Title;
        movie.ReleaseDate = updatedMovie.ReleaseDate;

        await movie.PopulateDirectorsAsync(updatedMovie.Directors, dbContext);
        await movie.PopulateActorsAsync(updatedMovie.Actors, dbContext);
        await movie.PopulateGenresAsync(updatedMovie.Genres, dbContext);
    }

}
