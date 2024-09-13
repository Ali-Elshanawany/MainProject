
namespace FinalBoatSystemRental.Application.Owner.Query;

public class ListPendingOwnersQuery : ICommand<IEnumerable<PendingOwnerViewModel>>
{
}

public class ListPendingOwnersHandler : IQueryHandler<ListPendingOwnersQuery, IEnumerable<PendingOwnerViewModel>>
{

    private readonly IOwnerRepository _OwnerRepository;
    private readonly IMapper _Mapper;

    public ListPendingOwnersHandler(IOwnerRepository ownerRepository, IMapper mapper)
    {
        _OwnerRepository = ownerRepository;
        _Mapper = mapper;
    }



    public async Task<IEnumerable<PendingOwnerViewModel>> Handle(ListPendingOwnersQuery request, CancellationToken cancellationToken)
    {
        var results = await _OwnerRepository.GetAllPendingOwners();
        return _Mapper.Map<IEnumerable<PendingOwnerViewModel>>(results);
    }
}
