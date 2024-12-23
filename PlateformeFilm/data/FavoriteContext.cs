using Microsoft.EntityFrameworkCore;
using PlateformeFilm.Models;

namespace PlateformeFilm.data
{
    public class FavoriteContext : DbContext
    {
        public FavoriteContext(DbContextOptions<FavoriteContext> options)
            : base(options)
        {
        }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Connexion a la base sqlite
        options.UseSqlite("Data Source=Favorite.db");
    }
        public DbSet<Favorite> Favorites { get; set; } = null!;
    }
}