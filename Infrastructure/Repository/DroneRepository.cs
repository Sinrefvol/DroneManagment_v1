using Domain.AggregatesModel.DroneAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DroneRepository : IDroneRepository
    {
        private readonly DroneContext _context;

        public DroneRepository(DroneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Drone Add(Drone drone)
        {
            return _context.Drones.Add(drone).Entity;
        }

        public async Task<Drone> GetById(Guid id)
        {
            var drone = await _context.Drones
                                .Include(d => d.DroneModel)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(d => d.Id == id);
            return drone;
        }

        public Drone Remove(Drone drone)
        {
            return _context.Drones.Remove(drone).Entity;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Drone UpdateDrone(Drone drone)
        {
            return _context.Drones.Update(drone).Entity;
        }
    }
}
