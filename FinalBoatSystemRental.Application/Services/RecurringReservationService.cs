namespace FinalBoatSystemRental.Application.Services;

public class RecurringReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public RecurringReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }


    public async Task UpdateExpiredReservationsAsync()
    {
        var now = DateTime.Now;
        var expiredReservations = await _reservationRepository.GetPendingReservation();

        foreach (var reservation in expiredReservations)
        {
            if (reservation.Trip.CancellationDeadLine <= now)
            {
                await Console.Out.WriteLineAsync($"Dead line is  {reservation.Trip.CancellationDeadLine} || now is {now}");
                reservation.Status = GlobalVariables.ReservationConfirmedStatus;
            }
        }

        await _reservationRepository.UpdateRangeAsync(expiredReservations);

    }


}

