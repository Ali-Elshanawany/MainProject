namespace FinalBoatSystemRental.Core.ViewModels.Owner;

public class Result
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static Result Success() => new Result { IsSuccess = true };
    public static Result Failure(string message) => new Result { IsSuccess = false, Message = message };
}
