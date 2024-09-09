//using Hangfire;
//using Microsoft.EntityFrameworkCore;

//namespace FinalBoatSystemRental.Application.Services;

//public class ReservationService
//{


//    private readonly IReservationRepository _reservationRepository;

//    public ReservationService(IReservationRepository reservationRepository)
//    {
//        _reservationRepository = reservationRepository;
//    }

//    public void ScheduleReservation(int reservationId, DateTime deadlineDate)
//    {
//        var delay = deadlineDate - DateTime.Now;

//        if (delay > TimeSpan.Zero)
//        {
//            BackgroundJob.Schedule(() => UpdateReservationStatusAsync(reservationId, deadlineDate), delay);
//        }
//        else
//        {
//            // If the deadline has already passed, update immediately
//            BackgroundJob.Enqueue(() => UpdateReservationStatusAsync(reservationId, deadlineDate));
//        }
//    }

//    public async Task UpdateReservationStatusAsync(int tripId, DateTime deadlineDate)
//    {
//        var trip = await _reservationRepository.GetByIdAsync(tripId);

//        if (trip == null || trip.Status == GlobalVariables.TripCompletedStatus)
//            return;

//        if (trip.Status == GlobalVariables.ReservationPendingStatus)
//        {
//            if (deadlineDate <= DateTime.Now)
//            {
//                trip.Status = "Completed";
//                await _reservationRepository.UpdateAsync(trip.Id, trip);
//            }
//        }


//    }


//}
