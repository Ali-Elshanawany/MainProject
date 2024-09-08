using AutoMapper;
using FinalBoatSystemRental.Application.Boat.ViewModels;

namespace FinalBoatSystemRental.Application.Boat.Query.List;

public class ListAvailableBoatsQuery : ICommand<IEnumerable<BoatViewModel>>
{

}
public class ListAvailableBoatsHandler : IQueryHandler<ListAvailableBoatsQuery, IEnumerable<BoatViewModel>>
{
    private readonly IBoatRepository _boatRepository;
    private readonly IMapper _mapper;

    public ListAvailableBoatsHandler(IBoatRepository boatRepository, IMapper mapper)
    {
        _boatRepository = boatRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<BoatViewModel>> Handle(ListAvailableBoatsQuery request, CancellationToken cancellationToken)
    {
        var boats = await _boatRepository.GetAllAvailableBoatsAsync();
        return _mapper.Map<IEnumerable<BoatViewModel>>(boats);
    }
}