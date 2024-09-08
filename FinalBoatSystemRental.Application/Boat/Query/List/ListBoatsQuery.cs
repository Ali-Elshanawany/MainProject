using AutoMapper;
using FinalBoatSystemRental.Application.Boat.ViewModels;
using FinalBoatSystemRental.Core.ViewModels.Boat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Application.Boat.Query.List;

public class ListBoatsQuery:ICommand<IEnumerable<AddBoatViewModel>>
{
    public string UserId { get; set; }=string.Empty;
    public ListBoatsQuery(string userId)
    {
        UserId = userId;
    }
}

public class ListBoatsHandler : IQueryHandler<ListBoatsQuery, IEnumerable<AddBoatViewModel>>
{

    private readonly IBoatRepository _boatRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public ListBoatsHandler(IBoatRepository boatRepository, IMapper mapper, IOwnerRepository ownerRepository)
    {
        _boatRepository = boatRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }


    public async Task<IEnumerable<AddBoatViewModel>> Handle(ListBoatsQuery request, CancellationToken cancellationToken)
    {
        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);

        var boats = await _boatRepository.GetAllAsync(ownerId);
        return _mapper.Map<IEnumerable<AddBoatViewModel>>(boats);
    }
}
