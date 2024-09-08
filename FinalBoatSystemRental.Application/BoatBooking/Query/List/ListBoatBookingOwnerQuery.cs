using FinalBoatSystemRental.Core.ViewModels.BoatBooking;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.BoatBooking.Query.List;

public class ListBoatBookingOwnerQuery : ICommand<IEnumerable<ListBoatBookingOwner>>
{
    [JsonIgnore]
    public string? UserId { get; set; }

    public ListBoatBookingOwnerQuery(string? userId)
    {
        UserId = userId;
    }
}

public class ListBoatBookingOwnerHandler : IQueryHandler<ListBoatBookingOwnerQuery, IEnumerable<ListBoatBookingOwner>>
{

    private readonly IOwnerRepository _ownerRepository;
    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IMapper _mapper;

    public ListBoatBookingOwnerHandler(IBoatBookingRepository boatBookingRepository, IMapper mapper, IOwnerRepository ownerRepository)
    {
        _boatBookingRepository = boatBookingRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<ListBoatBookingOwner>> Handle(ListBoatBookingOwnerQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            throw new Exception("unAuthorized Access");
        }

        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);

        var result = await _boatBookingRepository.GetBoatBookingOwner(ownerId);
        if (result != null)
        {
            return _mapper.Map<IEnumerable<ListBoatBookingOwner>>(result);
        }
        else
        {
            throw new Exception("There is no data!!");
        }
    }
}
