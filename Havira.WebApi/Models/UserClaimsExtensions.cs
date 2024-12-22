using System.Security.Claims;

namespace WebApi.Models;

public static class UserClaimsExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "0");
    }
}