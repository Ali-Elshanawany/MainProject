


namespace FinalBoatSystemRental.Application.Services;

public class AuthService : IAuthService
{

    private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
    private readonly JWT _jwt;
    private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;

    public AuthService(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IOptions<JWT> jwt,
        Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager = null)
    {
        _userManager = userManager;
        _jwt = jwt.Value;
        _roleManager = roleManager;
    }


    public async Task<string> AddRoleAsync(AddRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
            return "Invalid User Id or Role";

        if (await _userManager.IsInRoleAsync(user, model.Role))
            return "User Already assigned to this role ";

        var result = await _userManager.AddToRoleAsync(user, model.Role);

        return result.Succeeded ? string.Empty : "Something Went Wrong";

    }

    public async Task<AuthModel> Login(TokenRequestModel model)
    {
        var authModel = new AuthModel();
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            authModel.Message = "Email or password is incorrect!";
            return authModel;
        }

        var jwtSecurityToken = await CreateJwtTokenAsync(user);
        var roleList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email;
        authModel.UserName = user.UserName;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = roleList.ToList();

        return authModel;
    }

    // For now Register member (Customer)
    public async Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) is not null)
            return new AuthModel { Message = "Email is Already registered!" };

        if (await _userManager.FindByNameAsync(model.UserName) is not null)
            return new AuthModel { Message = "UserName is Already registered!" };

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description}";
            }
            return new AuthModel { Message = errors };
        }
        await _userManager.AddToRoleAsync(user, GlobalVariables.Customer);

        var jwtSecurityToken = await CreateJwtTokenAsync(user);

        return new AuthModel
        {
            Email = user.Email,
            UserName = user.UserName,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = new List<string> { GlobalVariables.Customer },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Message = "Register Successfully"
        };
    }

    public async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials
            );

        return jwtSecurityToken;

    }
}
