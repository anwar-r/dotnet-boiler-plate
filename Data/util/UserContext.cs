using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Data.util;

public interface IUserContext
{
    long CurrentUserId { get; }
}

public class UserContext : IUserContext
{
    public long CurrentUserId { get; }

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        CurrentUserId = GetClaimValue<long>("UserId",contextAccessor.HttpContext);
    }
    
    private static T? GetClaimValue<T>(string claimName,HttpContext? context)
    {
        var userClaims = context?.User.Claims.ToArray<Claim>();

        if (userClaims == null || userClaims.Length == 0)
            return default;

        var value = userClaims.FirstOrDefault(x => x.Type == claimName)?.Value;

        return (T)Convert.ChangeType(value, typeof(T));
    }
}