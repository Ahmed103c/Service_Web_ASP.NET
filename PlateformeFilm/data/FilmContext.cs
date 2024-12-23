using Microsoft.EntityFrameworkCore;
using PlateformeFilm.Models;

namespace PlateformeFilm.data
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> options)
            : base(options)
        {
        }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Connexion a la base sqlite
        options.UseSqlite("Data Source=Film.db");
    }
        public DbSet<Film> Films { get; set; } = null!;
    }
}