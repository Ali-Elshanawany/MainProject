using FinalBoatSystemRental.Core.ViewModels.Reservation;
using System.Text.Json.Serialization;


namespace FinalBoatSystemRental.Application.Reservation.Query.List;



public class ListCanceledOwnerReservationQuery : ICommand<IEnumerable<ReservationViewModel>>
{
    [JsonIgnore]
    public string? UserId { get; set; }

    public ListCanceledOwnerReservationQuery(string? userId)
    {
        UserId = userId;
    }
}

public class ListCanceledOwnerReservationQueryHandler : IQueryHandler<ListCanceledOwnerReservationQuery, IEnumerable<ReservationViewModel>>
{

    private readonly IOwnerRepository _ownerRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ListCanceledOwnerReservationQueryHandler(IMapper mapper, IOwnerRepository ownerRepository, IReservationRepository reservationRepository)
    {

        _mapper = mapper;
        _ownerRepository = ownerRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<ReservationViewModel>> Handle(ListCanceledOwnerReservationQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            throw new Exception("unAuthorized Access");
        }

        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);

        var result = await _reservationRepository.GetCanceledOwnerReservation(ownerId);
        if (result != null)
        {
            return _mapper.Map<IEnumerable<ReservationViewModel>>(result);
        }
        else
        {
            throw new Exception("There is no data!!");
        }
    }
}
