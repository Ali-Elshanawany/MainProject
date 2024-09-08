
using FinalBoatSystemRental.Application.Owner.Command.Add;
using FinalBoatSystemRental.Core.ViewModels.Owner;

namespace FinalBoatSystemRental.Application.Boat.Command.Add;

public class AddBoatCommandValidator : AbstractValidator<AddBoatCommand>
{
    public AddBoatCommandValidator()
    {

       
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null.")
            .NotEmpty().WithMessage("Name is Required");

          RuleFor(x => x.Description)
            .NotNull().WithMessage("Description cannot be null.")
            .NotEmpty().WithMessage("Description is Required");
        RuleFor(x => x.Capacity)
           .NotNull().WithMessage("Capacity cannot be null.")
           .NotEmpty().WithMessage("Capacity is Required")
           .GreaterThan(0).WithMessage("ReservationPrice Must be Greater Than Zero");


        RuleFor(x => x.ReservationPrice)
            .NotNull().WithMessage("ReservationPrice cannot be null.")
            .NotEmpty().WithMessage("ReservationPrice is Required")
            .GreaterThan(0).WithMessage("ReservationPrice Must be Greater Than Zero");

        RuleFor(x => x.MaxCancellationDateInDays)
            .NotNull().WithMessage("ReservationPrice cannot be null.")
            .NotEmpty().WithMessage("ReservationPrice is Required")
            .GreaterThan(0).WithMessage("MaxCancellationDateInDays Must be Greater Than Zero");




    }
}




public class AddBoatCommand:ICommand<AddBoatViewModel>
{
   
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int MaxCancellationDateInDays { get; set; }
    public decimal ReservationPrice { get; set; }
    
    public string? UserId { get; set; }

    public AddBoatCommand(string name, string description, int capacity, decimal reservationPrice, string userid, int maxCancellationDateInDays)
    {
        Name = name;
        Description = description;
        Capacity = capacity;
        ReservationPrice = reservationPrice;
        UserId = userid;
        MaxCancellationDateInDays = maxCancellationDateInDays;
    }
}

public class AddBoatHandler : ICommandHandler<AddBoatCommand, AddBoatViewModel>
{
    private readonly IBoatRepository _boatRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddBoatCommand> _validator;


    public AddBoatHandler(IBoatRepository boatRepository, IMapper mapper, IOwnerRepository ownerRepository, IValidator<AddBoatCommand> validator)
    {
        _boatRepository = boatRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
        _validator = validator;
    }

    public async Task<AddBoatViewModel> Handle(AddBoatCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        
        var ownerid = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var isBoatNameFound = await _boatRepository.CheckBoatName(ownerid, request.Name);
        if (isBoatNameFound)
            throw new Exception("Name Already Registered!;");

        var boat = new FinalBoatSystemRental.Core.Entities.Boat {
            Name = request.Name,
            Description = request.Description,
            Capacity = request.Capacity,
            ReservationPrice = request.ReservationPrice,
            CreatesAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = GlobalVariables.BoatPendingStatus,
            OwnerId = ownerid,
            MaxCancellationDateInDays= request.MaxCancellationDateInDays,
        };
        await _boatRepository.AddAsync(boat);
        return _mapper.Map<AddBoatViewModel>(boat);
    }
}
