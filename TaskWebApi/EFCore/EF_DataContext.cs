using Microsoft.EntityFrameworkCore;

namespace TaskWebApi.EFCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
