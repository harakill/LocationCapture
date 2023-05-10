using GeoCoordinatePortable;
using LocationCapture.Core;
using LocationCapture.Core.Domain;
using LocationCapture.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocationCapture.Persistence.Repositories
{
    public class PlacementRepository : Repository<Placement>, IPlacementRepository
    {
        public PlacementRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<double> CalculateSpeedOfMovement(int id1, int id2)
        {
            var speedOfMovement = 0.0;
            var placement1 = await ApplicationContext.Placements.FirstOrDefaultAsync(x => x.Id == id1);
            var placement2 = await ApplicationContext.Placements.FirstOrDefaultAsync(x => x.Id == id2);
            
            if (placement1 != null && placement2 != null && placement1.Vehicle == placement2.Vehicle)
            {
                var distance = DistanceBetween(placement1.Latitude, placement1.Longitude, 
                    placement2.Latitude, placement2.Longitude);

                var diffTimeStamp = placement2.TimeStamp - placement1.TimeStamp;

                speedOfMovement = distance / diffTimeStamp.Hours * 1000; // convert to KM/H
            }

            return speedOfMovement;
        }

        private static double DistanceBetween(
          double latitude1,
          double longitude1,
          double latitude2,
          double longitude2)
        {
            try
            {
                return new GeoCoordinate(latitude1, longitude1).GetDistanceTo(new GeoCoordinate(latitude2, longitude2));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ApplicationDbContext ApplicationContext
        {
            get { return Context; }
        }
    }
}
