using Microsoft.EntityFrameworkCore;
using PlateformeFilm.Models;
using PlateformeFilm.data;
using Microsoft.AspNetCore.Identity;
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

        builder.Services.AddDbContext<UserContext>();

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
