namespace FinalBoatSystemRental.Application.Owner.Command.Add
{
    public class VerifyOwnerCommand : IRequest<Result>
    {
        public int? OwnerId { get; set; }
        public VerifyOwnerCommand(int? ownerId)
        {
            OwnerId = ownerId;
        }
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

            if (request.OwnerId == null)
            {
                throw new Exception("Owner Id Can't be Null");
            }

            var owner = await _ownerRepository.GetByIdAsync((int)request.OwnerId);
            if (owner == null)
            {
                throw new Exception($"Owner Was not Found");
            }
            if (owner.IsVerified == true)
            {
                throw new Exception("Owner Already Verified");
            }
            owner.IsVerified = true;
            await _ownerRepository.UpdateAsync(owner.Id, owner);

            var res = new Result
            {
                IsSuccess = true,
                Message = "Owner Verified Successfully"
            };

            return res;
        }
    }

}
