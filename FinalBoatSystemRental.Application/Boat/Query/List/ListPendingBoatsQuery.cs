namespace FinalBoatSystemRental.Application.Boat.Query.List;

public class ListPendingBoatsQuery : ICommand<IEnumerable<UpdateBoatStatusViewModel>>
{


}
public class ListPendingBoatsHandler : IQueryHandler<ListPendingBoatsQuery, IEnumerable<UpdateBoatStatusViewModel>>
{
    private readonly IBoatRepository _boatRepository;
    private readonly IMapper _mapper;

    public ListPendingBoatsHandler(IBoatRepository boatRepository, IMapper mapper)
    {
        _boatRepository = boatRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<UpdateBoatStatusViewModel>> Handle(ListPendingBoatsQuery request, CancellationToken cancellationToken)
    {
        var boats = await _boatRepository.GetAllPendingBoatsAsync();
        return _mapper.Map<IEnumerable<UpdateBoatStatusViewModel>>(boats);
    }
}