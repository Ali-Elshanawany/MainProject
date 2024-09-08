using FinalBoatSystemRental.Application.Boat.Command.Add;

namespace FinalBoatSystemRental.Application.Customer.Command.Update;

public class UpdateCustomerDetailsCommandValidator : AbstractValidator<UpdateCustomerDetailsCommand>
{
    public UpdateCustomerDetailsCommandValidator()
    {


        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("FirstName cannot be null.")
            .NotEmpty().WithMessage("FirstName is Required");

        RuleFor(x => x.LastName)
           .NotNull().WithMessage("LastName cannot be null.")
           .NotEmpty().WithMessage("LastName is Required");

    }
}






public class UpdateCustomerDetailsCommand : ICommand<CustomerViewModel>
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    [JsonIgnore]
    public string? UserId { get; set; }

    public UpdateCustomerDetailsCommand(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

}

public class UpdateCustomerDetailsHandler : ICommandHandler<UpdateCustomerDetailsCommand, CustomerViewModel>
{

    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCustomerDetailsCommand> _validator;
    public UpdateCustomerDetailsHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateCustomerDetailsCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CustomerViewModel> Handle(UpdateCustomerDetailsCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var customer = await _customerRepository.GetCustomerByUserId(request.UserId);
        if (customer != null)
        {
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.UpdatedAt = DateTime.Now;
            await _customerRepository.UpdateAsync(customer.Id, customer);
            return _mapper.Map<CustomerViewModel>(customer);

        }
        throw new KeyNotFoundException("Customer Was not Found");

    }
}
