using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Api.Entities;

namespace MovieCollection.Api.Data;

public class MovieCollectionContext(DbContextOptions<MovieCollectionContext> options) 
            : DbContext (options)
{
    public DbSet<Movie> Movies => Set<Movie>();

    public DbSet<Director> Directors => Set<Director>();
    
    public DbSet<Actor> Actors => Set<Actor>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Github Copilot mener jeg trenger en modelBuilder for Movie entity også, men funker
        // fint uten enn så lenge??? Husk dette om noe går helt FUBAR


        // Seeder sjangre, statisk info
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Action" },
            new { Id = 2, Name = "Adventure" },
            new { Id = 3, Name = "Animation" },
            new { Id = 4, Name = "Anime" },
            new { Id = 5, Name = "Biography" },
            new { Id = 6, Name = "Comedy" },
            new { Id = 7, Name = "Crime" },
            new { Id = 8, Name = "Documentary" },
            new { Id = 9, Name = "Drama" },
            new { Id = 10, Name = "Family" },
            new { Id = 11, Name = "Fantasy" },
            new { Id = 12, Name = "Film-Noir" },
            new { Id = 13, Name = "History" },
            new { Id = 14, Name = "Horror" },
            new { Id = 15, Name = "Indie" },
            new { Id = 16, Name = "Music" },
            new { Id = 17, Name = "Musical" },
            new { Id = 18, Name = "Mystery" },
            new { Id = 19, Name = "Romance" },
            new { Id = 20, Name = "Science Fiction" },
            new { Id = 21, Name = "Short" },
            new { Id = 22, Name = "Sport" },
            new { Id = 23, Name = "Suspense" },
            new { Id = 24, Name = "Thriller" },
            new { Id = 25, Name = "TV Movie" },
            new { Id = 26, Name = "War" },
            new { Id = 27, Name = "Western" }
        );
    
    }

}

