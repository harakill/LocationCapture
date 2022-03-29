using LocationCapture.Core.Domain;

namespace LocationCapture.Core.Repositories
{
    public interface IPlacementRepository : IRepository<Placement>
    {
        Task<double> CalculateSpeedOfMovement(int id1, int id2);
    }
}
