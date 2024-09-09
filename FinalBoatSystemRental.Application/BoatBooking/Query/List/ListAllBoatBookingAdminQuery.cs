namespace FinalBoatSystemRental.Application.BoatBooking.Query.List;

public class ListAllBoatBookingAdminQuery : ICommand<IEnumerable<ListBoatBookingAdmin>>
{
}

public class ListAllBoatBookingAdminHandler : IQueryHandler<ListAllBoatBookingAdminQuery, IEnumerable<ListBoatBookingAdmin>>
{

    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IMapper _mapper;

    public ListAllBoatBookingAdminHandler(IBoatBookingRepository boatBookingRepository, IMapper mapper)
    {
        _boatBookingRepository = boatBookingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListBoatBookingAdmin>> Handle(ListAllBoatBookingAdminQuery request, CancellationToken cancellationToken)
    {
        var result = await _boatBookingRepository.GetAllBoatBookingAdmin();
        if (result != null)
        {
            return _mapper.Map<IEnumerable<ListBoatBookingAdmin>>(result);
        }
        else
        {
            throw new Exception("There is no data!!");
        }
    }
}