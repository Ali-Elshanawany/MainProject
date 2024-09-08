using AutoMapper;
using FinalBoatSystemRental.Application;
using FinalBoatSystemRental.Application.Trip.Query.Get;

namespace FinalBoatSystemRental.Application.Trip.Query.Get
{
    public class GetTripQuery:ICommand<TripViewModel>
    {

        public int TripId { get; set; }

        public string? UserId { get; set; }
        public GetTripQuery(int tripId, string userId)
        {
            TripId = tripId;
            UserId = userId;
        }


    }
}

public class GetTripHandler : IQueryHandler<GetTripQuery, TripViewModel>
{

    private readonly ITripRepository _tripRepository;
    private readonly IOwnerRepository _ownerRepository;

    private readonly IMapper _mapper;
    public GetTripHandler(ITripRepository tripRepository, IMapper mapper, IOwnerRepository ownerRepository)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }


    public async Task<TripViewModel> Handle(GetTripQuery request, CancellationToken cancellationToken)
    {
        var ownerid = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var trip =await _tripRepository.GetByIdAsync(request.TripId);
       
        if (trip == null)
        {
            throw new KeyNotFoundException("Trip was not found please try again!!");
        }
        else
        {
            return _mapper.Map<TripViewModel>(trip);
        }
    }
}
