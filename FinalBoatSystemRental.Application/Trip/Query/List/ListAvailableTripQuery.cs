namespace FinalBoatSystemRental.Application.Trip.Query.List;

public class ListAvailableTripQuery : ICommand<IEnumerable<AvailableTripsViewModel>>
{
}

public class ListAvailableTripHandler : IQueryHandler<ListAvailableTripQuery, IEnumerable<AvailableTripsViewModel>>
{

    private readonly ITripRepository _tripRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ListAvailableTripHandler(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AvailableTripsViewModel>> Handle(ListAvailableTripQuery request, CancellationToken cancellationToken)
    {
        var trips = await _tripRepository.GetTripsWithAvailableSeats();
        return _mapper.Map<List<AvailableTripsViewModel>>(trips);
    }
}