
namespace FinalBoatSystemRental.Application.Owner.Command.Add;



public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
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

public class RegisterCustomerCommand : ICommand<AuthModel>
{
    public RegisterModel Model { get; set; }
    public RegisterCustomerCommand(RegisterModel model)
    {
        Model = model;
    }

}

public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, AuthModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<RegisterCustomerCommand> _validator;

    public RegisterCustomerCommandHandler(UserManager<ApplicationUser> userManager,
                                            ICustomerRepository customerRepository, IValidator<RegisterCustomerCommand> validator)
    {
        _userManager = userManager;
        _customerRepository = customerRepository;
        _validator = validator;
    }


    public async Task<AuthModel> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }



        var model = request.Model;
        if (await _userManager.FindByEmailAsync(model.Email) is not null)
        {
            return new AuthModel { Message = "Email is already registered" };
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return new AuthModel { Message = "UserName is already registered" };
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

        await _userManager.AddToRoleAsync(user, GlobalVariables.Customer);
        var customer = new Core.Entities.Customer
        {
            UserId = user.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
        await _customerRepository.AddAsync(customer);
        return new AuthModel { Message = "Customer registered successfully", UserName = model.UserName, Email = model.Email, Roles = { GlobalVariables.Customer } };
    }
}
