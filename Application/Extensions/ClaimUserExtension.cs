
using System.Security.Claims;

namespace Application.Extensions;

public static class ClaimUserExtension
{

    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
