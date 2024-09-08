using AutoMapper;
using FinalBoatSystemRental.Core.Entities;
using FinalBoatSystemRental.Core.Interfaces;
using FinalBoatSystemRental.Core.ViewModels.Reservation;

namespace FinalBoatSystemRental.Application.Reservation.Command;

public class AddReservationCommand:ICommand<ReservationViewModel>
{
    public int BoatId { get; set; }
    public int TripId { get; set; }
    public int NumOfPeople { get; set; }
    public string? UserId { get; set; }
    public Dictionary<int, int> AdditionsQuantityIds { get; set; }

    public AddReservationCommand(int boatId, string? userId, Dictionary<int, int> additionsQuantityIds, int tripId, int numOfPeople)
    {
        BoatId = boatId;
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



    public AddReservationHandler(IReservationRepository reservationRepository, IReservationAdditionRepository reservationAdditionRepository,
        IOwnerRepository ownerRepository, IMapper mapper, IAdditionRepository additionRepository, ITripRepository tripRepository, ICustomerRepository customerRepository)
    {
        _reservationRepository = reservationRepository;
        _reservationAdditionRepository = reservationAdditionRepository;
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _additionRepository = additionRepository;
        _tripRepository = tripRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ReservationViewModel> Handle(AddReservationCommand request, CancellationToken cancellationToken)
    {
        // Get the total Addition price
        var additionsPrice = await _additionRepository.GetAdditionPrice(request.AdditionsQuantityIds.Keys.ToList());
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
        var trip = await _tripRepository.GetReservationTripPrice(request.TripId);
        var totalTripPriceWithOutAddition=trip.Price * request.NumOfPeople;

        //calculate the Cost of the Reservation
        var totalReservationPrice = TotalAdditionPrice + totalTripPriceWithOutAddition;

        var customer = await _customerRepository.GetCustomerByUserId(request.UserId);
        var isBalancedSufficient = await _customerRepository.CheckCustomerBalance(customer.Id, totalReservationPrice);
        if (!isBalancedSufficient)
        {
            throw new Exception("Balance Was not Enough To Book ");
        }
        customer.WalletBalance-=totalReservationPrice;
        await _customerRepository.UpdateAsync(customer.Id, customer);

        var status = GlobalVariables.DetermineBoatBookingStatus(trip.CancellationDeadLine);
        var reservation = new Core.Entities.Reservation
        {
           CustomerId= customer.Id,
           TripId=request.TripId,
           BoatId=request.BoatId,
           NumOfPeople=request.NumOfPeople,
           TotalPrice= (int) totalReservationPrice,
           ReservationDate=trip.ReservationDate,
           Status=status,
           CreatesAt=DateTime.Now,
           UpdatedAt=DateTime.Now, 
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

        return _mapper.Map<ReservationViewModel>(reservation);




    }
}