


using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalBoatSystemRental.Application.Owner.Command.Add;

public class PayRefundCommand : ICommand<Result>
{
    [Range(0, int.MaxValue, ErrorMessage = "cancellation Id must be greater than 0")]
    public int? CancellationId { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public PayRefundCommand(int? cancellationId, string userId)
    {
        CancellationId = cancellationId;
        UserId = userId;
    }
}

public class PayRefundHandler : ICommandHandler<PayRefundCommand, Result>
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICancellationRepository _cancellationRepository;
    private readonly IMapper _mapper;

    public PayRefundHandler(ICustomerRepository customerRepository, IOwnerRepository ownerRepository,
                            IMapper mapper, ICancellationRepository cancellationRepository)
    {
        _customerRepository = customerRepository;
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _cancellationRepository = cancellationRepository;
    }

    public async Task<Result> Handle(PayRefundCommand request, CancellationToken cancellationToken)
    {
        if (request.CancellationId == null)
        {
            throw new NullReferenceException("CancellationId can't be Null");
        }
        var CancellationId = (int)request.CancellationId;

        var cancellationData = await _cancellationRepository.GetByIdAsync(CancellationId);
        if (cancellationData == null)
        {
            throw new Exception("Cancellation Data Was not Found ");
        }
        if (cancellationData.RefundedYet)
        {
            throw new Exception("ALready Refunded!!!");
        }
        var customer = await _customerRepository.GetByIdAsync(cancellationData.CustomerId);
        if (customer == null)
        {
            throw new Exception("Customer Was Not Found");
        }
        var owner = await _ownerRepository.GetByUserId(request.UserId);
        if (owner.WalletBalance < cancellationData.RefundAmount)
        {
            throw new Exception("your available balance doesn't cover the amount of the refund");
        }
        owner.WalletBalance -= cancellationData.RefundAmount;
        customer.WalletBalance += cancellationData.RefundAmount;
        cancellationData.RefundedYet = true;
        await _ownerRepository.UpdateAsync(owner.Id, owner);
        await _customerRepository.UpdateAsync(customer.Id, customer);
        await _cancellationRepository.UpdateAsync(cancellationData.Id, cancellationData);
        var result = new Result
        {
            IsSuccess = true,
            Message = $"Refunded Successfully Your Balance now is {owner.WalletBalance}"
        };

        return _mapper.Map<Result>(result);
    }
}