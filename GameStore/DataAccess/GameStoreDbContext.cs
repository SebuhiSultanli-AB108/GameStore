using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess
{
    public class GameStoreDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public GameStoreDbContext(DbContextOptions opt) : base(opt) { }
    }
}
