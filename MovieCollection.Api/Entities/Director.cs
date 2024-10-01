using System;

namespace MovieCollection.Api.Entities;

public class Director
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Movie> Movies { get; set; } = new List<Movie>();

}
