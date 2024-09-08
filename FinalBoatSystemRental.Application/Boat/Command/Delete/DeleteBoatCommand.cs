using FinalBoatSystemRental.Application.Boat.ViewModels;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Boat.Command.Delete;



public class DeleteBoatCommandValidator : AbstractValidator<DeleteBoatCommand>
{
    public DeleteBoatCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}


public class DeleteBoatCommand : ICommand<BoatViewModel>
{
    public int? Id { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }

    public DeleteBoatCommand(int? id, string userId)
    {
        Id = id;
        UserId = userId;
    }
}

public class DeleteBoatHandler : ICommandHandler<DeleteBoatCommand, BoatViewModel>
{

    private readonly IBoatRepository _boatRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IBoatBookingRepository _boatBookingRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<DeleteBoatCommand> _validator;

    public DeleteBoatHandler(IBoatRepository boatRepository, ITripRepository tripRepository,
                            IBoatBookingRepository boatBookingRepository, IMapper mapper,
                            IOwnerRepository ownerRepository, IValidator<DeleteBoatCommand> validator)
    {
        _boatRepository = boatRepository;
        _tripRepository = tripRepository;
        _boatBookingRepository = boatBookingRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
        _validator = validator;
    }

    public async Task<BoatViewModel> Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        var boatId = (int)request.Id;

        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var boat = await _boatRepository.GetByIdAsync(boatId);
        if (boat == null)
        {
            throw new Exception("boat Not Found ");
        }

        var isOwner = await _boatRepository.IsOwner(boatId, ownerId);
        if (!isOwner)
        {
            throw new UnauthorizedAccessException("You can't access this data (you are not the owner of the boat!!)");
        }
        // Make Sure That  there is no Trip Associated With That Boat With Status ( Confirmed or active ) 
        var isTripFound = await _tripRepository.GetConfirmedTripAsync(boatId);
        if (isTripFound)
        {
            throw new Exception("Can not Remove The boat Has Reservation Found");
        }
        // Make Sure That  there is no Boat Booking Associated With That Boat With Status ( Confirmed or active ) 
        var isBoatFound = await _boatBookingRepository.IsBoatBookingFound(boatId);
        if (isBoatFound)
        {
            throw new Exception("Can not Remove The boat Has Reservation Found");
        }

        await _boatRepository.DeleteAsync(boatId);
        return _mapper.Map<BoatViewModel>(boat);
    }
}
