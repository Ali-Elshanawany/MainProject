using AutoMapper;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Customer.Command.Update;

public class UpdateCustomerWalletCommand : ICommand<CustomerViewModel>
{
    public decimal? WalletBalance { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }

    public UpdateCustomerWalletCommand(decimal? walletBalance, string? userId)
    {
        WalletBalance = walletBalance;
        UserId = userId;
    }
}

public class UpdateCustomerWalletHandler : ICommandHandler<UpdateCustomerWalletCommand, CustomerViewModel>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerWalletHandler(ICustomerRepository customerRepository = null, IMapper mapper = null)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerViewModel> Handle(UpdateCustomerWalletCommand request, CancellationToken cancellationToken)
    {



        var customer = await _customerRepository.GetCustomerByUserId(request.UserId);
        if (request.WalletBalance <= 0 || request.WalletBalance == null)
        {
            throw new InvalidDataException("Balance can not be less or equal ");
        }
        var walletBalance = (decimal)request.WalletBalance;
        customer.WalletBalance += walletBalance;
        await _customerRepository.UpdateAsync(customer.Id, customer);
        return _mapper.Map<CustomerViewModel>(customer);

    }
}