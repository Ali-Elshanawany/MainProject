namespace FinalBoatSystemRental.Application.Reservation.Query.List;

public class ListAllTripReservationAdminQuery : ICommand<IEnumerable<AdminReservationViewModel>>
{
}


public class ListAllTripReservationAdminHandler : IQueryHandler<ListAllTripReservationAdminQuery, IEnumerable<AdminReservationViewModel>>
{

    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ListAllTripReservationAdminHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AdminReservationViewModel>> Handle(ListAllTripReservationAdminQuery request, CancellationToken cancellationToken)
    {
        var results = await _reservationRepository.GetTripReservationAdmin();
        return _mapper.Map<IEnumerable<AdminReservationViewModel>>(results);
    }
}