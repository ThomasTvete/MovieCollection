using System;

namespace MovieCollection.Api.Entities;

public class Movie
{
    public int Id { get; set; }

    public required string Title {get; set; }

    public List<Director> Directors {get; set;} = new List<Director>();

    public List<Actor> Actors {get; set;} = new List<Actor>();

    //many-to-many
    public List<Genre> Genres {get; set;} = new List<Genre>();

    // public int GenreId {get; set;}

    // public Genre? Genre { get; set;}

    public DateOnly ReleaseDate {get; set;}
    

    // Liste opp directors/actors/genres som int (for id) og string (for navn), for å 
    // initialisere disse egenskapene når Movie instansen lages, slipper å kjøre .Select
    // hver gang en request trenger MovieSummaryDto eller MovieDetailsDto

    // NOPE, dårlig ide, bare MapPost som lager en helt ny Movie instans uansett

//     public List<int> DirectorIds {get; set;} = new List<int>();
//     public List<int> ActorIds {get; set;} = new List<int>();
//     public List<int> GenreIds {get; set;} = new List<int>();

//     public List<string> DirectorNames {get; set;} = new List<string>();
//     public List<string> ActorNames {get; set;} = new List<string>();
//     public List<string> GenreNames {get; set;} = new List<string>();
}
