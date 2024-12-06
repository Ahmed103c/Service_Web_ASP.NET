using Microsoft.EntityFrameworkCore;
using PlateformeFilm.Models;

namespace PlateformeFilm.data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Connexion a la base sqlite
        options.UseSqlite("Data Source=User.db");
    }
        public DbSet<User> Users { get; set; } = null!;
    }
}
