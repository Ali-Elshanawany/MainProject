
namespace FinalBoatSystemRental.Application.Boat.Command.Update;

public class UpdateBoatStatusCommand : ICommand<UpdateBoatStatusViewModel>
{
    public int? BoatId { get; set; }



    public UpdateBoatStatusCommand(int? boatId)
    {
        BoatId = boatId;
    }
}

public class UpdateBoatStatusCommandHandler : ICommandHandler<UpdateBoatStatusCommand, UpdateBoatStatusViewModel>
{

    private readonly IBoatRepository _boatRepository;
    private readonly IMapper _mapper;
    public UpdateBoatStatusCommandHandler(IBoatRepository boatRepository, IMapper mapper)
    {
        _boatRepository = boatRepository;
        _mapper = mapper;
    }

    public async Task<UpdateBoatStatusViewModel> Handle(UpdateBoatStatusCommand request, CancellationToken cancellationToken)
    {
        if (request.BoatId == null)
        {
            throw new Exception("Boat Id is required can't be Null ");
        }

        var boatId = (int)request.BoatId;

        var boat = await _boatRepository.GetByIdAsync(boatId);

        if (boat == null)
            throw new Exception("Boat was not Found");

        boat.Status = GlobalVariables.BoatApprovedStatus;
        await _boatRepository.UpdateAsync(boatId, boat);
        return _mapper.Map<UpdateBoatStatusViewModel>(boat);
    }
}