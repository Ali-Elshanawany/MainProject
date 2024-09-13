using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReservationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        /*
         * Search For Reservation in Specific Trip  
         */
        public async Task<bool> isReservationFound(int tripId)
        {
            return await _dbContext.Reservations
                .AsNoTracking()
                .AnyAsync(a => a.TripId == tripId);
        }

        // Return Reservation History For Customer
        public async Task<IEnumerable<Reservation>> GetTripReservationCustomerHistory(int customerId)
        {
            var now = DateTime.Now.Date; // Cache current date to avoid multiple calls
            return await _dbContext.Reservations.AsNoTracking()
                                         .Where(i => i.CustomerId == customerId)
                                         .OrderByDescending(i => i.CreatesAt)

                                         .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetTripReservationAdmin()
        {

            return await _dbContext.Reservations.AsNoTracking()
                                         .Where(i => i.Status != GlobalVariables.ReservationCanceledStatus)
                                         .Include(i => i.Boat)
                                         .Include(i => i.Customer)
                                         .ToListAsync();
        }
        public async Task<IEnumerable<Reservation>> GetPendingReservation()
        {

            return await _dbContext.Reservations
                                         .Where(i => i.Status == GlobalVariables.ReservationPendingStatus)
                                         .Include(i => i.Trip)
                                         .ToListAsync();
        }
        public async Task<IEnumerable<Reservation>> GetCanceledTripReservationAdmin()
        {

            return await _dbContext.Reservations.AsNoTracking()
                                         .Where(i => i.Status == GlobalVariables.ReservationCanceledStatus)
                                         .Include(i => i.Boat)
                                         .Include(i => i.Customer)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetOwnerReservation(int ownerId)
        {
            return await _dbContext.Reservations.AsNoTracking()
                                  .Include(i => i.Boat)
                                  .Where(i => i.Boat.OwnerId == ownerId &&
                                              (i.Status != GlobalVariables.ReservationCanceledStatus)).OrderByDescending(a => a.ReservationDate)
                                  .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetCanceledOwnerReservation(int ownerId)
        {
            return await _dbContext.Reservations.AsNoTracking()
                               .Include(i => i.Boat)
                               .Where(i => i.Boat.OwnerId == ownerId &&
                                           (i.Status == GlobalVariables.ReservationCanceledStatus)).OrderByDescending(a => a.ReservationDate)
                               .ToListAsync();
        }
    }
}
