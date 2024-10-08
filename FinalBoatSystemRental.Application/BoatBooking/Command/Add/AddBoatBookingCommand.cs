﻿namespace FinalBoatSystemRental.Application.BoatBooking.Command.Add;



public class AddBoatBookingCommandValidator : AbstractValidator<AddBoatBookingCommand>
{
    public AddBoatBookingCommandValidator()
    {


        RuleFor(x => x.BookingDate)
            .NotNull().WithMessage("BookingDate cannot be null.")
            .NotEmpty().WithMessage("BookingDate is Required")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Booking Date Must Be Greater than Todays Date");

        RuleFor(x => x.BoatId)
          .NotNull().WithMessage("BoatId cannot be null.")
          .NotEmpty().WithMessage("BoatId is Required");

        RuleFor(x => x.AdditionsQuantityIds)
           .NotNull().WithMessage("Additions cannot be null.");






    }
}


public class AddBoatBookingCommand : ICommand<BoatBookingViewModel>
{

    public DateTime? BookingDate { get; set; }
    public int? BoatId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public Dictionary<int, int>? AdditionsQuantityIds { get; set; }

    public AddBoatBookingCommand(DateTime? bookingDate, int? boatId, string userId, Dictionary<int, int>? additionsQuantityIds)
    {
        BookingDate = bookingDate;
        BoatId = boatId;
        UserId = userId;
        AdditionsQuantityIds = additionsQuantityIds;
    }

}

public class AddBoatBookingHandler : ICommandHandler<AddBoatBookingCommand, BoatBookingViewModel>
{
    private readonly IBoatBookingRepository _boatBooking;
    private readonly IBoatBookingAdditionRepository _bookingAdditionRepository;
    private readonly IAdditionRepository _additionRepository;
    private readonly IBoatRepository _boatRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddBoatBookingCommand> _validator;

    public AddBoatBookingHandler(IAdditionRepository additionRepository, IMapper mapper, IBoatRepository boatRepository,
                                ICustomerRepository customerRepository, IBoatBookingRepository boatBooking, IBoatBookingAdditionRepository bookingAdditionRepository,
                                IValidator<AddBoatBookingCommand> validator)
    {
        _additionRepository = additionRepository;
        _mapper = mapper;
        _boatRepository = boatRepository;
        _customerRepository = customerRepository;
        _boatBooking = boatBooking;
        _bookingAdditionRepository = bookingAdditionRepository;
        _validator = validator;
    }



    public async Task<BoatBookingViewModel> Handle(AddBoatBookingCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Converting After Validation 
        var boatId = (int)request.BoatId;
        var bookingDate = (DateTime)request.BookingDate;


        // check the Date 
        var isDateAvailable = await _boatBooking.CheckBoatAvialableDate(boatId, bookingDate);
        if (isDateAvailable)
        {
            throw new Exception("Date is Already Booked");
        }
        // Get the total price
        var additionsPrice = await _additionRepository.GetAdditionPrice(request.AdditionsQuantityIds.Keys.ToList());
        var dictTotalAdditionPrice = new Dictionary<int, int>();
        var TotalAdditionPrice = 0;

        foreach (var addition in additionsPrice)
        {
            var id = addition.Id;
            var additionTotalPrice = addition.price * request.AdditionsQuantityIds[id];
            dictTotalAdditionPrice[id] = additionTotalPrice;
            TotalAdditionPrice += additionTotalPrice;
        }
        var boatPrice = await _boatRepository.GetReservationBoatPrice(boatId);
        var totalReservationPrice = TotalAdditionPrice + boatPrice;

        // check Customer Balance
        var customer = await _customerRepository.GetCustomerByUserId(request.UserId);
        var isBalancedSufficient = await _customerRepository.CheckCustomerBalance(customer.Id, totalReservationPrice);
        if (!isBalancedSufficient)
        {
            throw new Exception("Balance Was not Enough To Book ");
        }
        customer.WalletBalance -= totalReservationPrice;
        await _customerRepository.UpdateAsync(customer.Id, customer);


        var status = GlobalVariables.DetermineBoatBookingStatus(bookingDate);
        var reservation = new Core.Entities.BoatBooking
        {
            CustomerId = customer.Id,
            BoatId = boatId,
            BookingDate = bookingDate,
            TotalPrice = totalReservationPrice,
            Status = status,
            CreatesAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        await _boatBooking.AddAsync(reservation);

        var addRange = new List<BoatBookingAddition>();
        foreach (var item in dictTotalAdditionPrice)
        {
            var newBookBookingAddition = new BoatBookingAddition
            {
                AdditionId = item.Key,
                BoatBookingId = reservation.Id,
                Quantity = request.AdditionsQuantityIds[item.Key],
                TotalPrice = item.Value,
                CreatedAt = DateTime.Now,
            };
            addRange.Add(newBookBookingAddition);

        }
        await _bookingAdditionRepository.AddRange(addRange);

        return _mapper.Map<BoatBookingViewModel>(reservation);
    }
}