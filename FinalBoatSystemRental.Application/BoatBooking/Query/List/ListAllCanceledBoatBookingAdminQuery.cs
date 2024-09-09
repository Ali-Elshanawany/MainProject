namespace FinalBoatSystemRental.Application.BoatBooking.Query.List;

public class ListAllCanceledBoatBookingAdminQuery : ICommand<IEnumerable<ListBoatBookingAdmin>>
{
}

public class ListAllCanceledBoatBookingAdminHandler : IQueryHandler<ListAllCanceledBoatBookingAdminQuery, IEnumerable<ListBoatBookingAdmin>>
{

    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IMapper _mapper;

    public ListAllCanceledBoatBookingAdminHandler(IBoatBookingRepository boatBookingRepository, IMapper mapper)
    {
        _boatBookingRepository = boatBookingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListBoatBookingAdmin>> Handle(ListAllCanceledBoatBookingAdminQuery request, CancellationToken cancellationToken)
    {
        var result = await _boatBookingRepository.GetAllCanceledBoatBookingAdmin();
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