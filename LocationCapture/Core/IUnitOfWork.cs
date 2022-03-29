using LocationCapture.Core.Repositories;

namespace LocationCapture.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPlacementRepository Placements { get; }
        Task CompleteAsync();
    }
}
