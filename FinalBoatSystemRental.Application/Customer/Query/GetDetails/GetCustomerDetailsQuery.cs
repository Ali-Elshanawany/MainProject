using AutoMapper;

namespace FinalBoatSystemRental.Application.Customer.Query.GetDetails;

public class GetCustomerDetailsQuery:ICommand<CustomerViewModel>
{
    public string? UserId { get; set; }

    public GetCustomerDetailsQuery(string userId)
    {
        UserId = userId;
    }

}

public class GetCustomerDetailsHandler : ICommandHandler<GetCustomerDetailsQuery, CustomerViewModel>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IMapper _mapper;
    public GetCustomerDetailsHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerViewModel> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        var customer=await _customerRepository.GetCustomerByUserId(request.UserId);
        if (customer != null)
        {
            return _mapper.Map<CustomerViewModel>(customer);
        }
        throw new KeyNotFoundException("Customer Data was not found Please try again!!");
    }
}