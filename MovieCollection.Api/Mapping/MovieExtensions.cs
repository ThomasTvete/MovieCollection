using System;
using Microsoft.EntityFrameworkCore; 
using MovieCollection.Api.Data;
using MovieCollection.Api.Dtos;
using MovieCollection.Api.Entities;

namespace MovieCollection.Api.Mapping;

public static class MovieExtensions
{
    public static async Task PopulateDirectorsAsync(this Movie movieEntity, List<CreateDirectorDto> directors, MovieCollectionContext dbContext)
    {
        foreach (CreateDirectorDto directorDto in directors)
        {

          
                Director? director;
                if (directorDto.Id.HasValue)
                {
                    director = await dbContext.Directors.FindAsync(directorDto.Id.Value);
                }
                else
                {   //sjekker om noen med samme navn allerede finnes i Directors tabellen
                    director = await dbContext.Directors.FirstOrDefaultAsync(d => d.Name == directorDto.Name);
                    if(director is null)
                    //legger til director i tabellen
                    {
                        director = new Director
                        {
                            Name = directorDto.Name!, //null-forgiving fordi når det først når hit så er Name Required
                        };
                        dbContext.Directors.Add(director);
                        await dbContext.SaveChangesAsync(); //lagrer den oppdaterte tabellen
                    }
                }
            
            movieEntity.Directors.Add(director!); //setter director inn i film objektets liste
            
        }

    }

    public static async Task PopulateActorsAsync(this Movie movieEntity, List<CreateActorDto> actors, MovieCollectionContext dbContext)
    {
        foreach (CreateActorDto actorDto in actors)
        {

                Actor? actor;
                if (actorDto.Id.HasValue)
                {
                    actor = await dbContext.Actors.FindAsync(actorDto.Id.Value);
                }
                else
                {   
                    actor = await dbContext.Actors.FirstOrDefaultAsync(a => a.Name == actorDto.Name);
                    if(actor is null)
                    {
                        actor = new Actor
                        {
                            Name = actorDto.Name!, 
                        };
                        dbContext.Actors.Add(actor);
                        await dbContext.SaveChangesAsync(); 
                    }
                }
            
            movieEntity.Actors.Add(actor!); 
        }

    }

    public static async Task PopulateGenresAsync(this Movie movieEntity, List<int> genres, MovieCollectionContext dbContext)
    {
        foreach (int genre in genres)
        {
            //Genres tabellen skal ikke utvides, så den legges bare til filmen om sjangeren allerede eksisterer
            Genre? genreCheck = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genre);
            if (genreCheck is not null)
            {
                movieEntity.Genres.Add(genreCheck);
            }
            else
            {
                //NB en eller annen feilmelding om at genreCheck ikke eksisterer
                // Results.NotFound(genreCheck)? sjekk om det funker sånn (les mer om Results)
            }
        }

    }

}
