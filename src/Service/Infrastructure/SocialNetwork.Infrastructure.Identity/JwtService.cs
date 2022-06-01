using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.Infrastructure.Identity;

public class JwtService
{
    private readonly IdentitySettings _siteSetting;

    private IUserClaimsPrincipalFactory<AppUser> _claimsPrincipal;

    public JwtService(
            IOptions<IdentitySettings> siteSetting,
            UserManager<AppUser> userManager,
            IUserClaimsPrincipalFactory<AppUser> claimsPrincipal)
    {
        _siteSetting = siteSetting.Value;
        _claimsPrincipal = claimsPrincipal;
    }
    public async Task<string> GenerateAsync(AppUser user)
    {
        var secretKey = Encoding.UTF8.GetBytes(_siteSetting.SecretKey); // longer that 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.Encryptkey); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


        var claims = await _claimsPrincipal.CreateAsync(user);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _siteSetting.Issuer,
            Audience = _siteSetting.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(0),
            Expires = DateTime.Now.AddMinutes(_siteSetting.ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims.Claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

}
