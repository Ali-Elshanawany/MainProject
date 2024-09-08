

using System.IdentityModel.Tokens.Jwt;

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IAuthService
{

    Task<AuthModel> RegisterAsync(RegisterModel model);
    Task<AuthModel> Login(TokenRequestModel model);

    Task<String> AddRoleAsync(AddRoleModel model);

    Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user);

}
