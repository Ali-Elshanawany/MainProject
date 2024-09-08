//using AutoMapper;
//using FinalBoatSystemRental.Application.Boat.ViewModels;

//namespace FinalBoatSystemRental.Application.Boat.Command.Update;

//public class UpdateBoatCommand:ICommand<UpdateBoatViewModel>
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public string Description { get; set; } = string.Empty;
//    public int Capacity { get; set; }
//    public decimal ReservationPrice { get; set; }

//    public UpdateBoatCommand(int id,string name,string description,int capacity,decimal reservationPrice)
//    {
//        Id = id;
//        Name = name;
//        Description = description;
//        Capacity = capacity;
//        ReservationPrice = reservationPrice;
//    }

//}

//public class UpdateBoatHandler : ICommandHandler<UpdateBoatCommand, UpdateBoatViewModel>
//{
//    private readonly IBoatRepository _boatRepository;
//    private readonly IMapper _mapper;

//    public UpdateBoatHandler(IMapper mapper, IBoatRepository boatRepository)
//    {
//        _mapper = mapper;
//        _boatRepository = boatRepository;
//    }

//    public async Task<UpdateBoatViewModel> Handle(UpdateBoatCommand request, CancellationToken cancellationToken)
//    {
//        var boat = await _boatRepository.GetByIdAsync(request.Id);
//        if (boat != null)
//        {
//            boat.Name = request.Name;
//            boat.Description = request.Description;
//            boat.ReservationPrice = request.ReservationPrice;
//            boat.Capacity = request.Capacity;
//            boat.UpdatedAt= DateTime.Now;

//            await _boatRepository.UpdateAsync(boat.Id, boat);
//            return _mapper.Map<UpdateBoatViewModel>(boat);
//        }
//        throw new KeyNotFoundException("boat was not found");
//    }
//}