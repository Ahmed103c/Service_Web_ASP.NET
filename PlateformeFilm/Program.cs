using Microsoft.EntityFrameworkCore;
using PlateformeFilm.Models;
using PlateformeFilm.data;
using Microsoft.AspNetCore.Identity;
using PlateformeFilm.Services;
namespace PlateformeFilm;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Injection de dépendance : ajout classe PasswordHasher
        builder.Services.AddScoped<PasswordHasher<User>>();
        //Injection de dépendance : ajout HttpClient
        builder.Services.AddHttpClient();
        //Injection de dépendance : ajout classe OmdbService
        builder.Services.AddSingleton<Omdbservice>();

        
        //Connection au BDD
        builder.Services.AddDbContext<UserContext>();
        //Ajout Films et Favorites
        builder.Services.AddDbContext<FilmContext>();
        //puis les commandes powershell : 
        //$ dotnet ef migrations add InitialisationDeLaDB --context FilmContext
        //$ dotnet ef database update --context FilmContext

        builder.Services.AddDbContext<FavoriteContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
