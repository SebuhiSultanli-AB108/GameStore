using GameStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public GameStoreDbContext(DbContextOptions opt) : base(opt) { }

    }
}