

namespace FinalBoatSystemRental.Application;

public static class GlobalVariables
{

    // Roles 
    public const string Admin = "Admin";
    public const string AdminPassword = "Password123!";
    public const string Customer = "Customer";
    public const string Owner = "Owner";

    public static string NormalizedAdmin = Admin.ToUpper();
    public static string NormalizedCustomer = Customer.ToUpper();
    public static string NormalizedOwner = Owner.ToUpper();

    // Error Messages
    public static string EmailIsRegistered = "Email is already registered";
    public static string UserNameIsRegistered = "Username is already registered";

    // Boat Status

    public static string BoatApprovedStatus = "Approved";
    public static string BoatPendingStatus = "Pending";
    public static string BoatRejectedStatus = "Rejected";


    // Trip Status

    public static string TripCompletedStatus = "Completed";
    public static string TripActiveStatus = "Active";
    public static string TripCanceledStatus = "Canceled";

    // BoatBooking Status

    public static string BoatBookingConfirmedStatus = "Confirmed";
    public static string BoatBookingPendingStatus = "Pending";
    public static string BoatBookingCanceledStatus = "Canceled";

    // Reservation Status

    public static string ReservationConfirmedStatus = "Confirmed";
    public static string ReservationPendingStatus = "Pending";
    public static string ReservationCanceledStatus = "Canceled";



    // Num of Days Before Deadline
    public static int maxCancellationDays = 2;

    // Determine Boat BookingStatus
    public static string DetermineBoatBookingStatus(DateTime bookingDate)
    {
        if (bookingDate.AddDays(-maxCancellationDays) > DateTime.Now)
        {
            return GlobalVariables.BoatBookingPendingStatus;
        }
        else
        {
            return GlobalVariables.BoatBookingConfirmedStatus;
        }
    }


}
