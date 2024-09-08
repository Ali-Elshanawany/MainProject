using AutoMapper;
using FinalBoatSystemRental.Core.ViewModels.Reservation;

namespace FinalBoatSystemRental.Application.Reservation.Query.List;

public class ListReservationCustomerHistoryQuery : ICommand<IEnumerable<ReservationViewModel>>
{
    public string? UserId { get; set; }

    public ListReservationCustomerHistoryQuery(string? userId)
    {
        UserId = userId;
    }
}

public class ListReservationCustomerHistoryHandler : IQueryHandler<ListReservationCustomerHistoryQuery, IEnumerable<ReservationViewModel>>
{

    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public ListReservationCustomerHistoryHandler(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }




    public async Task<IEnumerable<ReservationViewModel>> Handle(ListReservationCustomerHistoryQuery request, CancellationToken cancellationToken)
    {
        var customerId = await _customerRepository.GetCustomerIdByUserId(request.UserId);
        var results = await _reservationRepository.GetTripReservationCustomerHistory(customerId);
        return _mapper.Map<IEnumerable<ReservationViewModel>>(results);
    }
}