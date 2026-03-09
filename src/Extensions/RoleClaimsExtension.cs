using Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Iat,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));

        return result;
    }
}
