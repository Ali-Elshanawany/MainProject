namespace FinalBoatSystemRental.Application.Owner.Query.GetDetails;

public class GetOwnerDetailsQuery : ICommand<OwnerViewModel>
{
    public string UserId { get; set; }

    public GetOwnerDetailsQuery(string userId)
    {

        UserId = userId;
    }
}
public class GetOwnerDetailsHandler : IQueryHandler<GetOwnerDetailsQuery, OwnerViewModel>
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public GetOwnerDetailsHandler(IMapper mapper, IOwnerRepository ownerRepository)
    {
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }

    public async Task<OwnerViewModel> Handle(GetOwnerDetailsQuery request, CancellationToken cancellationToken)
    {
        var owner = await _ownerRepository.GetByUserId(request.UserId);
        if (owner != null)
        {
            return _mapper.Map<OwnerViewModel>(owner);
        }
        throw new KeyNotFoundException("Owner Was not Found Please Try Again!");
    }
}