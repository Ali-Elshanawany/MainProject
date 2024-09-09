namespace FinalBoatSystemRental.Application.Reservation.Query.List;

public class ListAllCanceledTripReservationAdminQuery : ICommand<IEnumerable<AdminReservationViewModel>>
{
}

public class ListAllCanceledTripReservationAdminHandler : IQueryHandler<ListAllCanceledTripReservationAdminQuery, IEnumerable<AdminReservationViewModel>>
{

    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ListAllCanceledTripReservationAdminHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AdminReservationViewModel>> Handle(ListAllCanceledTripReservationAdminQuery request, CancellationToken cancellationToken)
    {
        var results = await _reservationRepository.GetCanceledTripReservationAdmin();
        return _mapper.Map<IEnumerable<AdminReservationViewModel>>(results);
    }
}