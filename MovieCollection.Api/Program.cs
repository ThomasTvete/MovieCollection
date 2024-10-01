using MovieCollection.Api.Data;
using MovieCollection.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);


var connString = builder.Configuration.GetConnectionString("MovieCollection");
builder.Services.AddSqlite<MovieCollectionContext>(connString);

var app = builder.Build();

app.MapMoviesEndpoints();
app.MapGenresEndpoints();
app.MapActorsEndpoints();
app.MapDirectorsEndpoints();

await app.MigrateDbAsync();

app.Run();
