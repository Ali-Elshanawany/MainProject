using FluentValidation;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Trip.Command.Add;

public class AddTripCommandValidator : AbstractValidator<AddTripCommand>
{
    public AddTripCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required")
                            .MinimumLength(3).WithMessage("Trip Name is short");
        //.NotNull().WithMessage("Name Can't be Null");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is Required")
                            .MinimumLength(3).WithMessage("Trip Description is short");

        RuleFor(x => x.PricePerPerson).NotEmpty().WithMessage("PricePerPerson Is Required")
                                      .NotNull().WithMessage("Price Per Person can't be null")

                                      .GreaterThan(0).WithMessage("Price Must be greater than 0");

        RuleFor(x => x.MaxPeople).NotEmpty().WithMessage("Num of people Is Required")
                              .NotNull().WithMessage("Max Number of People can't be null")
                            .GreaterThan(0).WithMessage("Num of people Must be greater than 0");

        RuleFor(x => x.BoatId).NotEmpty().WithMessage("BoatId Is Required")
                              .NotNull().WithMessage("BoatId can't be null");


        RuleFor(x => x.CancellationDeadLine)
                 .LessThan(x => x.StartedAt).WithMessage("Cancellation deadline must be before the start of the trip.");



    }
    private bool Isint(string input)
    {
        return int.TryParse(input, out _);
    }

}

public class AddTripCommand : ICommand<TripViewModel>
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal? PricePerPerson { get; set; } = decimal.Zero;
    public int? MaxPeople { get; set; }
    public DateTime CancellationDeadLine { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.Now;

    public int? BoatId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }

    public AddTripCommand(string name, string description, decimal? pricePerPerson, int? maxPeople, DateTime cancellationDeadLine, DateTime startedAt, int? boatId, string userId)
    {
        Name = name;
        Description = description;
        PricePerPerson = pricePerPerson;
        MaxPeople = maxPeople;
        CancellationDeadLine = cancellationDeadLine;
        StartedAt = startedAt;
        BoatId = boatId;
        UserId = userId;
    }
}

public class AddTripHandler : ICommandHandler<AddTripCommand, TripViewModel>
{
    private readonly ITripRepository _tripRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IBoatRepository _boatRepository;
    private readonly IValidator<AddTripCommand> _validator;
    private readonly IMapper _mapper;
    public AddTripHandler(ITripRepository tripRepository, IMapper mapper, IOwnerRepository ownerRepository,
                            IBoatBookingRepository boatBookingRepository, IBoatRepository boatRepository, IValidator<AddTripCommand> validator)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
        _boatBookingRepository = boatBookingRepository;
        _boatRepository = boatRepository;
        _validator = validator;
    }

    public async Task<TripViewModel> Handle(AddTripCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        // Converting After Validation 
        var boatId = (int)request.BoatId;
        var pricePerPerson = (decimal)request.PricePerPerson;

        var ownerid = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var isOwner = await _boatRepository.IsOwner(boatId, ownerid);
        if (!isOwner)
        {
            throw new UnauthorizedAccessException("You can't access this data (you are not the owner of the boat!!)");
        }

        var isTripFound = await _tripRepository.isTripAvailable(boatId, request.StartedAt);
        if (isTripFound)
        {
            throw new Exception($"Can not Add This Trip on boat {request.BoatId} Another Trip Already Registered At {request.StartedAt}");
        }

        var isBoatBookingFound = await _boatBookingRepository.CheckBoatAvialableDate(boatId, request.StartedAt);
        if (isBoatBookingFound)
        {
            throw new Exception($"Can not Add This Trip on boat {request.BoatId} Another boat Booking Already Registered At {request.StartedAt}");
        }

        var trip = new Core.Entities.Trip
        {
            Name = request.Name,
            Description = request.Description,
            PricePerPerson = pricePerPerson,
            MaxPeople = request.MaxPeople,
            CancellationDeadLine = request.CancellationDeadLine,
            Status = GlobalVariables.TripActiveStatus,
            StartedAt = request.StartedAt,
            BoatId = boatId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            OwnerId = ownerid

        };
        await _tripRepository.AddAsync(trip);
        return _mapper.Map<TripViewModel>(trip);

    }
}