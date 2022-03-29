using LocationCapture.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace LocationCapture.Core
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Placement> Placements { get; set; }

        Task SaveChangesAsync();
    }
}
