using FinalBoatSystemRental.Core.ViewModels.BoatBooking;

namespace FinalBoatSystemRental.Application.BoatBooking.Query.List;

public class ListBoatBookingHistoryQuery : ICommand<IEnumerable<BoatBookingHistoryViewModel>>
{
    public string? UserId { get; set; }

    public ListBoatBookingHistoryQuery(string? userId)
    {
        UserId = userId;
    }
}

public class ListBoatBookingHistoryHandler : IQueryHandler<ListBoatBookingHistoryQuery, IEnumerable<BoatBookingHistoryViewModel>>
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IMapper _mapper;

    public ListBoatBookingHistoryHandler(ICustomerRepository customerRepository, IBoatBookingRepository boatBookingRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _boatBookingRepository = boatBookingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BoatBookingHistoryViewModel>> Handle(ListBoatBookingHistoryQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            throw new Exception("unAuthorized Access");
        }
        var customerId = await _customerRepository.GetCustomerIdByUserId(request.UserId);


        var result = await _boatBookingRepository.GetBoatBookingCustomerHistory(customerId);
        if (result != null)
        {
            return _mapper.Map<IEnumerable<BoatBookingHistoryViewModel>>(result);
        }
        else
        {
            throw new Exception("There is no data!!");
        }
    }
}

