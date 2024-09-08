using AutoMapper;

namespace FinalBoatSystemRental.Application.Trip.Query.List;

public class ListTripQuery : ICommand<IEnumerable<TripViewModel>>
{
    public string UserId { get; set; } = string.Empty;
    public ListTripQuery(string userId)
    {
        UserId = userId;
    }
}

public class ListTripsHandler : IQueryHandler<ListTripQuery, IEnumerable<TripViewModel>>
{

    private readonly ITripRepository _tripRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public ListTripsHandler(IMapper mapper, IOwnerRepository ownerRepository, ITripRepository tripRepository)
    {   
        _mapper = mapper;
        _ownerRepository = ownerRepository;
        _tripRepository = tripRepository;
    }


    public async Task<IEnumerable<TripViewModel>> Handle(ListTripQuery request, CancellationToken cancellationToken)
    {
        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var trips = await _tripRepository.GetAllAsync(ownerId);
        return _mapper.Map<IEnumerable<TripViewModel>>(trips);


       
    }
}