
using FinalBoatSystemRental.Core.ViewModels.BoatBooking;

namespace FinalBoatSystemRental.Application.BoatBooking.Query.List;



public class ListCanceledBoatBookingOwnersQuery : ICommand<IEnumerable<ListBoatBookingOwner>>
{
    public string? UserId { get; set; }

    public ListCanceledBoatBookingOwnersQuery(string? userId)
    {
        UserId = userId;
    }
}

public class ListCanceledBoatBookingOwnersHandler : IQueryHandler<ListCanceledBoatBookingOwnersQuery, IEnumerable<ListBoatBookingOwner>>
{

    private readonly IOwnerRepository _ownerRepository;
    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IMapper _mapper;

    public ListCanceledBoatBookingOwnersHandler(IBoatBookingRepository boatBookingRepository, IMapper mapper, IOwnerRepository ownerRepository)
    {
        _boatBookingRepository = boatBookingRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<ListBoatBookingOwner>> Handle(ListCanceledBoatBookingOwnersQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            throw new Exception("unAuthorized Access");
        }

        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);

        var result = await _boatBookingRepository.GetCanceledBoatBookingOwner(ownerId);
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
