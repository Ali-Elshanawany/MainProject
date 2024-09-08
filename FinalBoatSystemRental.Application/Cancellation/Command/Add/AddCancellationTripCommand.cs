using AutoMapper;
using FinalBoatSystemRental.Core.ViewModels.Cancellation;

namespace FinalBoatSystemRental.Application.Cancellation.Command.Add;

public class AddCancellationTripCommand:ICommand<CancellationViewModel>
{
    public int ReservationId { get; set; }

    public AddCancellationTripCommand(int reservationId)
    {
        ReservationId = reservationId;
    }

}

public class AddCancellationTripHandler : ICommandHandler<AddCancellationTripCommand, CancellationViewModel>
{

    private readonly ICancellationRepository _cancellationRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public AddCancellationTripHandler(ICancellationRepository cancellationRepository, ITripRepository tripRepository,
                                      IReservationRepository reservationRepository ,IMapper mapper)
    {
        _cancellationRepository = cancellationRepository;
        _tripRepository = tripRepository;
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }


    public async Task<CancellationViewModel> Handle(AddCancellationTripCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
        if (reservation.Status == GlobalVariables.ReservationCanceledStatus)
        {
            throw new Exception("Reservation Already Canceled!!");
        }
        var tripDeadLine = await _tripRepository.GetReservationTripDeadline(reservation.TripId);

        reservation.Status=GlobalVariables.ReservationCanceledStatus;
        reservation.CanceledAt=DateTime.Now;

        if (tripDeadLine.Date > DateTime.Now.Date)// if true then Refundable 
        {
            var cancellation = new Core.Entities.Cancellation
            {
                CustomerId = reservation.CustomerId,
                ReservationId = reservation.Id,
                BoatBookingId = null,
                CancellationDate = DateTime.Now,
                RefundAmount = reservation.TotalPrice,
                RefundedYet=false,
                CreatedAt = DateTime.Now
            };

            await _cancellationRepository.AddAsync(cancellation);
            await _reservationRepository.UpdateAsync(reservation.Id,reservation);
            return _mapper.Map<CancellationViewModel>(cancellation);
        }
        else
        {
            var cancellation = new Core.Entities.Cancellation
            {
                CustomerId = reservation.CustomerId,
                ReservationId = reservation.Id,
                BoatBookingId = null,
                CancellationDate = DateTime.Now,
                RefundAmount = 0,
                RefundedYet=true,
                CreatedAt = DateTime.Now
            };

            await _cancellationRepository.AddAsync(cancellation);
            await _reservationRepository.UpdateAsync(reservation.Id, reservation);
            return _mapper.Map<CancellationViewModel>(cancellation);
        }

    }
}