
using FinalBoatSystemRental.Application.Owner.Command.Update;

namespace FinalBoatSystemRental.Application.Owner.Command.Add;


public class RegisterOwnerCommandValidator : AbstractValidator<RegisterOwnerCommand>
{
    public RegisterOwnerCommandValidator()
    {

        RuleFor(x => x.Model.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email is Required");
        RuleFor(x => x.Model.Password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password is Required")
            .Matches("[A-Z]").WithMessage("Passwords must have at least one uppercase ('A'-'Z')")
            .Matches("[0-9]").WithMessage("Passwords must have at least one digit ('0'-'9')")
            .Matches("[^a-zA-Z0-9]").WithMessage("Passwords must have at least one special character.");
        RuleFor(x => x.Model.UserName)
            .NotNull().WithMessage("UserName cannot be null.")
            .NotEmpty().WithMessage("UserName is Required");
        RuleFor(x => x.Model.FirstName)
            .NotNull().WithMessage("FirstName cannot be null.")
            .NotEmpty().WithMessage("FirstName is Required");
        RuleFor(x => x.Model.LastName)
            .NotNull().WithMessage("LastName cannot be null.")
            .NotEmpty().WithMessage("LastName is Required");

    }
}


public class RegisterOwnerCommand : ICommand<AuthModel>
{
    public RegisterModel? Model { get; set; }
    public RegisterOwnerCommand(RegisterModel model)
    {
        Model = model;
    }

}

public class RegisterOwnerCommandHandler : ICommandHandler<RegisterOwnerCommand, AuthModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IValidator<RegisterOwnerCommand> _validator;

    public RegisterOwnerCommandHandler(UserManager<ApplicationUser> userManager, IOwnerRepository ownerRepository, IValidator<RegisterOwnerCommand> validator)
    {
        _userManager = userManager;
        _ownerRepository = ownerRepository;
        _validator = validator;
    }


    public async Task<AuthModel> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }



        var model = request.Model;
        if (await _userManager.FindByEmailAsync(model.Email) is not null)
        {
            return new AuthModel { Message = GlobalVariables.EmailIsRegistered };
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return new AuthModel { Message = GlobalVariables.UserNameIsRegistered };
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return new AuthModel { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
        }

        await _userManager.AddToRoleAsync(user, GlobalVariables.Owner);
        var owner = new Core.Entities.Owner
        {
            IsVerified = false,
            UserId = user.Id,
        };
        await _ownerRepository.AddAsync(owner);
        return new AuthModel { Message = "Owner registered successfully" };
    }
}
