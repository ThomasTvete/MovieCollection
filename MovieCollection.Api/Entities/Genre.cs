using System;

namespace MovieCollection.Api.Entities;

public class Genre
{
    public int Id { get; set; }

    public required string Name { get; set; }

    //many-to-many
    public List<Movie> Movies {get; set;} = new List<Movie>();
}
