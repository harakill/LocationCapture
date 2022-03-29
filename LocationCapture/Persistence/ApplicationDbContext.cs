using LocationCapture.Core;
using LocationCapture.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LocationCapture.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) 
            : base(contextOptions) { }

        public virtual DbSet<Placement> Placements { get; set; }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
