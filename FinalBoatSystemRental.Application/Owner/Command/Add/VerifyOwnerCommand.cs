namespace FinalBoatSystemRental.Application.Owner.Command.Add
{
    public class VerifyOwnerCommand : IRequest<Result>
    {
        public int OwnerId { get; set; }
    }

    public class VerifyOwnerCommandHandler : IRequestHandler<VerifyOwnerCommand, Result>
    {
        private readonly IOwnerRepository _ownerRepository;

        public VerifyOwnerCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public async Task<Result> Handle(VerifyOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId);
            if (owner == null)
            {
                return Result.Failure("Owner not found");
            }
            owner.IsVerified = true;
            await _ownerRepository.UpdateAsync(owner.Id, owner);

            return Result.Success();
        }
    }

}
