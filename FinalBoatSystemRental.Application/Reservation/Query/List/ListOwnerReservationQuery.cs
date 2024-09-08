using FinalBoatSystemRental.Core.ViewModels.Reservation;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Reservation.Query.List;


public class ListOwnerReservationQuery : ICommand<IEnumerable<ReservationViewModel>>
{
    [JsonIgnore]
    public string? UserId { get; set; }

    public ListOwnerReservationQuery(string? userId)
    {
        UserId = userId;
    }
}

public class ListOwnerReservationQueryHandler : IQueryHandler<ListOwnerReservationQuery, IEnumerable<ReservationViewModel>>
{

    private readonly IOwnerRepository _ownerRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ListOwnerReservationQueryHandler(IMapper mapper, IOwnerRepository ownerRepository, IReservationRepository reservationRepository)
    {

        _mapper = mapper;
        _ownerRepository = ownerRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<ReservationViewModel>> Handle(ListOwnerReservationQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            throw new Exception("unAuthorized Access");
        }

        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);

        var result = await _reservationRepository.GetOwnerReservation(ownerId);
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
