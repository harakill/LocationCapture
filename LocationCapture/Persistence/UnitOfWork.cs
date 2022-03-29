using LocationCapture.Core;
using LocationCapture.Core.Repositories;
using LocationCapture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocationCapture.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context, IPlacementRepository placementRepository)
        {
            _context = context;
            Placements = placementRepository;
        }

        public IPlacementRepository Placements { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
