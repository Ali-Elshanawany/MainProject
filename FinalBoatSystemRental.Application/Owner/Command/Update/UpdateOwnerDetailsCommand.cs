using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Owner.Command.Update;

public class UpdateOwnerDetailsCommandValidator : AbstractValidator<UpdateOwnerDetailsCommand>
{
    public UpdateOwnerDetailsCommandValidator()
    {
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address Can't be Empty");
        RuleFor(x => x.BusinessName).NotEmpty().WithMessage("BusinessName Can't be Empty");
    }
}




public class UpdateOwnerDetailsCommand : ICommand<OwnerViewModel>
{
    public string? BusinessName { get; set; }
    public string? Address { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }

    public UpdateOwnerDetailsCommand(string? businessName, string? address, string? userId)
    {
        BusinessName = businessName;
        Address = address;
        UserId = userId;
    }
}

public class UpdateOwnerDetailsHandler : ICommandHandler<UpdateOwnerDetailsCommand, OwnerViewModel>
{
    private readonly IOwnerRepository _ownerRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<UpdateOwnerDetailsCommand> _validator;
    public UpdateOwnerDetailsHandler(IOwnerRepository ownerRepository, IMapper mapper, IValidator<UpdateOwnerDetailsCommand> validator)
    {
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _validator = validator;
    }



    public async Task<OwnerViewModel> Handle(UpdateOwnerDetailsCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        var owner = await _ownerRepository.GetByUserId(request.UserId);
        if (owner != null)
        {
            owner.BusinessName = request.BusinessName;
            owner.Address = request.Address;
            await _ownerRepository.UpdateAsync(owner.Id, owner);
            return _mapper.Map<OwnerViewModel>(owner);
        }
        throw new KeyNotFoundException("Owner Was Not Found ");
    }
}