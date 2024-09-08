
namespace FinalBoatSystemRental.Application.Owner.Command.Add;

public class RegisterCustomerCommand:ICommand<AuthModel>
{
    public RegisterModel Model {  get; set; }
    public RegisterCustomerCommand(RegisterModel model)
    {
        Model = model;
    }

}

public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, AuthModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICustomerRepository _customerRepository;

    public RegisterCustomerCommandHandler(UserManager<ApplicationUser> userManager, ICustomerRepository customerRepository)
    {
        _userManager = userManager;
        _customerRepository = customerRepository;
    }


    public async Task<AuthModel> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
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
        return new AuthModel { Message = "Customer registered successfully" };
    } 
}
