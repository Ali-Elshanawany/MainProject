using FinalBoatSystemRental.Application.Services;

namespace FinalBoatSystemRental.Application.Reservation.Command;


public class AddReservationCommandValidator : AbstractValidator<AddReservationCommand>
{
    public AddReservationCommandValidator()
    {
        RuleFor(x => x.TripId)
            .NotNull().WithMessage("TripId cannot be null.")
            .NotEmpty().WithMessage("TripId is Required");

        RuleFor(x => x.NumOfPeople)
          .NotNull().WithMessage("NumOfPeople cannot be null.")
          .NotEmpty().WithMessage("NumOfPeople is Required")
          .GreaterThan(0).WithMessage("Num of people Must be greater than 0");
    }
}




public class AddReservationCommand : ICommand<ReservationViewModel>
{
    // public int BoatId { get; set; }
    public int? TripId { get; set; }
    public int? NumOfPeople { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public Dictionary<int, int> AdditionsQuantityIds { get; set; }

    public AddReservationCommand(string? userId, Dictionary<int, int> additionsQuantityIds, int? tripId, int? numOfPeople)
    {
        //   BoatId = boatId;
        UserId = userId;
        AdditionsQuantityIds = additionsQuantityIds;
        TripId = tripId;
        NumOfPeople = numOfPeople;
    }
}

public class AddReservationHandler : ICommandHandler<AddReservationCommand, ReservationViewModel>
{

    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationAdditionRepository _reservationAdditionRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IAdditionRepository _additionRepository;
    private readonly ITripRepository _tripRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddReservationCommand> _validator;



    public AddReservationHandler(IReservationRepository reservationRepository, IReservationAdditionRepository reservationAdditionRepository,
        IOwnerRepository ownerRepository, IMapper mapper, IAdditionRepository additionRepository, ITripRepository tripRepository,
        ICustomerRepository customerRepository, IValidator<AddReservationCommand> validator)
    {
        _reservationRepository = reservationRepository;
        _reservationAdditionRepository = reservationAdditionRepository;
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _additionRepository = additionRepository;
        _tripRepository = tripRepository;
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<ReservationViewModel> Handle(AddReservationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Converting 
        var NumOfPeople = (int)request.NumOfPeople;
        var TripId = (int)request.TripId;


        var trip = await _tripRepository.GetByIdAsync(TripId);
        if (trip == null)
            throw new Exception("Trip was not found");


        // Get the total Addition price
        var additionsPrice = await _additionRepository.GetAdditionPrice(request.AdditionsQuantityIds.Keys.ToList());
        //if (!additionsPrice.Any())
        //    throw new Exception("Additions Was not Found");
        var dictTotalAdditionPrice = new Dictionary<int, int>();
        var TotalAdditionPrice = 0;

        // Storing the Total price Per Addition For later saving in Reservation Addition Table
        foreach (var addition in additionsPrice)
        {
            var id = addition.Id;
            var additionTotalPrice = addition.price * request.AdditionsQuantityIds[id];
            dictTotalAdditionPrice[id] = additionTotalPrice;
            TotalAdditionPrice += additionTotalPrice;
        }

        // trip contain  price,Cancellation Deadline and ReservationDate
        var totalTripPriceWithOutAddition = trip.PricePerPerson * request.NumOfPeople;

        //calculate the Cost of the Reservation
        var totalReservationPrice = TotalAdditionPrice + (decimal)totalTripPriceWithOutAddition;

        var customer = await _customerRepository.GetCustomerByUserId(request.UserId);
        var isBalancedSufficient = await _customerRepository.CheckCustomerBalance(customer.Id, totalReservationPrice);
        if (!isBalancedSufficient)
        {
            throw new Exception("Balance Was not Enough To Book ");
        }
        customer.WalletBalance -= totalReservationPrice;
        await _customerRepository.UpdateAsync(customer.Id, customer);

        // change Trip Status
        var availableSeats = await _tripRepository.GetAvailableSeats(TripId);
        if (availableSeats == request.NumOfPeople)
        {

            trip.Status = GlobalVariables.TripCompletedStatus;
            await _tripRepository.UpdateAsync(trip.Id, trip);

        }

        var status = GlobalVariables.DetermineBoatBookingStatus(trip.CancellationDeadLine);
        var reservation = new Core.Entities.Reservation
        {
            CustomerId = customer.Id,
            TripId = TripId,
            BoatId = trip.BoatId,
            NumOfPeople = NumOfPeople,
            TotalPrice = (int)totalReservationPrice,
            ReservationDate = trip.StartedAt,
            Status = status,
            CreatesAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        await _reservationRepository.AddAsync(reservation);

        var addRange = new List<ReservationAddition>();
        foreach (var item in dictTotalAdditionPrice)
        {
            var newBookBookingAddition = new ReservationAddition
            {
                AdditionId = item.Key,
                ReservationId = reservation.Id,
                Quantity = request.AdditionsQuantityIds[item.Key],
                TotalPrice = item.Value,
                CreatedAt = DateTime.Now,
            };
            addRange.Add(newBookBookingAddition);

        }
        await _reservationAdditionRepository.AddRange(addRange);

        //ReservationService reservationService = new ReservationService(_reservationRepository);

        //reservationService.ScheduleReservation(reservation.Id, trip.CancellationDeadLine);


        return _mapper.Map<ReservationViewModel>(reservation);




    }
}