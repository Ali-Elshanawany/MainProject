using AutoMapper;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Customer.Command.Update;

public class UpdateCustomerDetailsCommand : ICommand<CustomerViewModel>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [JsonIgnore]
    public string? UserId { get; set; }

}

public class UpdateCustomerDetailsHandler : ICommandHandler<UpdateCustomerDetailsCommand, CustomerViewModel>
{

    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;
    public UpdateCustomerDetailsHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerViewModel> Handle(UpdateCustomerDetailsCommand request, CancellationToken cancellationToken)
    {
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
