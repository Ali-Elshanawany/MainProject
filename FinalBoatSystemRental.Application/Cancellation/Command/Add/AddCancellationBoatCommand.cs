using AutoMapper;
using FinalBoatSystemRental.Core.ViewModels.Cancellation;

namespace FinalBoatSystemRental.Application.Cancellation.Command.Add;

public class AddCancellationBoatCommand : ICommand<CancellationViewModel>
{
    public int BookingId { get; set; }

    public AddCancellationBoatCommand(int bookingId)
    {
        BookingId = bookingId;
    }
}



public class AddCancellationBoatHandler : ICommandHandler<AddCancellationBoatCommand, CancellationViewModel>
{

    private readonly ICancellationRepository _cancellationRepository;
    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IBoatRepository _boatRepository;
    private readonly IMapper _mapper;

    public AddCancellationBoatHandler(ICancellationRepository cancellationRepository, IBoatBookingRepository boatBookingRepository, IBoatRepository boatRepository, IMapper mapper)
    {
        _cancellationRepository = cancellationRepository;
        _boatBookingRepository = boatBookingRepository;
        _boatRepository = boatRepository;
        _mapper = mapper;
    }


    public async Task<CancellationViewModel> Handle(AddCancellationBoatCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _boatBookingRepository.GetByIdAsync(request.BookingId);
        if (reservation.Status == GlobalVariables.BoatBookingCanceledStatus)
        {
            throw new Exception("Reservation Already Canceled!!");
        }
        var boatDeadline = await _boatRepository.GetMaxCancellationDateInDays(reservation.BoatId);

        reservation.Status = GlobalVariables.BoatBookingCanceledStatus;
        reservation.CanceledAt = DateTime.Now;

        if (reservation.BookingDate.AddDays(-boatDeadline).Date < DateTime.Now.Date)// if true then Refundable 
        {
            var cancellation = new Core.Entities.Cancellation
            {
                CustomerId = reservation.CustomerId,
                ReservationId = null,
                BoatBookingId = reservation.Id,
                CancellationDate = DateTime.Now,
                RefundAmount = reservation.TotalPrice,
                RefundedYet = false,
                CreatedAt = DateTime.Now
            };

            await _cancellationRepository.AddAsync(cancellation);
            await _boatBookingRepository.UpdateAsync(reservation.Id, reservation);
            return _mapper.Map<CancellationViewModel>(cancellation);
        }
        else
        {
            var cancellation = new Core.Entities.Cancellation
            {
                CustomerId = reservation.CustomerId,
                ReservationId = null,
                BoatBookingId = reservation.Id,
                CancellationDate = DateTime.Now,
                RefundAmount = 0,
                RefundedYet = true,
                CreatedAt = DateTime.Now
            };

            await _cancellationRepository.AddAsync(cancellation);
            await _boatBookingRepository.UpdateAsync(reservation.Id, reservation);
            return _mapper.Map<CancellationViewModel>(cancellation);
        }

    }
}