
using Microsoft.AspNetCore.Identity;

namespace FinalBoatSystemRental.Core;

public class ApplicationUser : IdentityUser
{

    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;

    public virtual Owner Owner { get; set; } = default!;
    public virtual Customer Customer { get; set; } = default!;


}
