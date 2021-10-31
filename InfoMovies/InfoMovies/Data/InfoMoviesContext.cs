using InfoMovies.Data.Dtos.Auth;
using InfoMovies.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfoMovies.Data
{
    public class InfoMoviesContext : IdentityDbContext<InfoMoviesUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = tcp:infomoviesdbserver.database.windows.net, 1433; Initial Catalog = InfoMovies_db; Persist Security Info = False; User ID = adminkar33; Password = Kopustai8. ; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; Trusted_Connection=False");
        }
    }
}
