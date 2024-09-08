using AutoMapper;
using FinalBoatSystemRental.Application.Boat.ViewModels;
using FinalBoatSystemRental.Core.ViewModels.Boat;

namespace FinalBoatSystemRental.Application.Boat.Query.Get;

public class GetBoatQuery:ICommand<AddBoatViewModel>
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public GetBoatQuery(int id, string userId)
    {
        Id = id;   
        UserId = userId;
    }
}

public class GetBoatHandler : IQueryHandler<GetBoatQuery, AddBoatViewModel>
{

    private readonly IBoatRepository _boatRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public GetBoatHandler(IBoatRepository boatRepository, IMapper mapper, IOwnerRepository ownerRepository)
    {
        _boatRepository = boatRepository;
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }

    public async Task<AddBoatViewModel> Handle(GetBoatQuery request, CancellationToken cancellationToken)
    {
        var ownerid = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var boat =await _boatRepository.GetByIdAsync(request.Id,ownerid);
        if (boat != null)
        {
            return _mapper.Map<AddBoatViewModel>(boat);
        }
        throw new KeyNotFoundException("Boat Was not Found Please Try Again!");
    }
}
