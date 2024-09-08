
using AutoMapper;
using FluentValidation;

namespace FinalBoatSystemRental.Application.Owner.Command.Update;

public class UpdateOwnerWalletCommandValidator : AbstractValidator<UpdateOwnerWalletCommand>
{
    public UpdateOwnerWalletCommandValidator()
    {
        RuleFor(x => x.WalletBalance).GreaterThan(0).NotEmpty().WithMessage("Wallet Balance Must be greater than zero.");

    }
}


public class UpdateOwnerWalletCommand : ICommand<OwnerViewModel>
{
    public decimal WalletBalance { get; set; }
    public string? UserId { get; set; }


    public UpdateOwnerWalletCommand(decimal walletBalance, string? userId)
    {
        WalletBalance = walletBalance;
        UserId = userId;
    }
}

public class UpdateOwnerWalletHandler : ICommandHandler<UpdateOwnerWalletCommand, OwnerViewModel>
{

    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateOwnerWalletCommand> _validator;
    public UpdateOwnerWalletHandler(IOwnerRepository ownerRepository, IMapper mapper, IValidator<UpdateOwnerWalletCommand> validator)
    {
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _validator = validator;
    }


    public async Task<OwnerViewModel> Handle(UpdateOwnerWalletCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        var owner = await _ownerRepository.GetByUserId(request.UserId);

        owner.WalletBalance += request.WalletBalance;
        await _ownerRepository.UpdateAsync(owner.Id, owner);
        return _mapper.Map<OwnerViewModel>(owner);
    }
}