

namespace FinalBoatSystemRental.Application.Owner.Command.Add;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {

        RuleFor(x => x.Email)
       .NotNull().WithMessage("Email cannot be null.") // Explicitly checks for null
       .NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Password is Required");

    }
}


public class LoginCommand : IRequest<AuthModel>
{

    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
}


public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IValidator<LoginCommand> _validator;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService
                                , IOwnerRepository ownerRepository, IValidator<LoginCommand> validator)
    {
        _userManager = userManager;
        _authService = authService;
        _ownerRepository = ownerRepository;
        _validator = validator;
    }
    public async Task<AuthModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }


        var authModel = new AuthModel();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            authModel.Message = "Email or password is incorrect";
            return authModel;
        }

        var owner = await _ownerRepository.GetByUserId(user.Id);
        if (user != null && owner != null && !owner.IsVerified)
        {
            authModel.Message = "owner account is not verified";
            return authModel;
        }


        var jwtToken = await _authService.CreateJwtTokenAsync(user);
        var roleList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.ExpiresOn = jwtToken.ValidTo;
        authModel.Email = user.Email;
        authModel.UserName = user.UserName;
        authModel.Roles = roleList.ToList();
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return authModel;
    }
}
