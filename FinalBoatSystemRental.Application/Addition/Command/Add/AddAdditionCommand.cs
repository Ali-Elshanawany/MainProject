
namespace FinalBoatSystemRental.Application.Addition.Command.Add
{
    public class AddAdditionCommandValidator : AbstractValidator<AddAdditionCommand>
    {
        public AddAdditionCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required")
                .MinimumLength(3).WithMessage("Minimum Name Length is 3");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required")
               .MinimumLength(3).WithMessage("Description Name Length is 3");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is Required")
               .GreaterThan(0).WithMessage("Price Must be greater than 0");

        }
    }


    public class AddAdditionCommand : ICommand<AdditionViewModel>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? UserId { get; set; }



        public AddAdditionCommand(string name, string description, int price, string userId)
        {
            Name = name;
            Description = description;
            Price = price;
            UserId = userId;
        }
    }
}

public class AddAdditionHandler : ICommandHandler<AddAdditionCommand, AdditionViewModel>
{

    private readonly IAdditionRepository _additionRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddAdditionCommand> _validator;

    public AddAdditionHandler(IAdditionRepository additionRepository, IOwnerRepository ownerRepository,
                            IMapper mapper, IValidator<AddAdditionCommand> validator)
    {
        _additionRepository = additionRepository;
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<AdditionViewModel> Handle(AddAdditionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        var ownerId = await _ownerRepository.GetOwnerIdByUserId(request.UserId);
        var isNameFound = await _additionRepository.CheckAdditionName(request.Name, ownerId);
        if (isNameFound)
        {
            throw new Exception("Name is ALready Registered plz Try Another Name ");
        }
        var addition = new Addition
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            OwnerId = ownerId,
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now,
        };
        await _additionRepository.AddAsync(addition);
        return _mapper.Map<AdditionViewModel>(addition);
    }
}
