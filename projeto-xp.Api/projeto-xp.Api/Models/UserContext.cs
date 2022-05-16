using Microsoft.EntityFrameworkCore;

namespace projeto_xp.Api.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<UserItemCreate> UserItems { get; set; } = null;
    }
}
