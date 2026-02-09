

using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

public class TokenService (IConfiguration conf)  : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = conf["TokenKey"] ?? throw new Exception("Cannot get tken key !");
        if (tokenKey.Length < 64) throw new Exception("Token key must be greater than or equal 64 !");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new (ClaimTypes.Email , user.Email),
            new (ClaimTypes.NameIdentifier , user.Id)
        };

        var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };


        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token); 

    }
}