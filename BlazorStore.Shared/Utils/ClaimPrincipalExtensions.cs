using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace BlazorStore.Shared.Utils
{
    public static class ClaimPrincipalExtensions
    {
        public static int GetId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new AuthenticationException($"Required {ClaimTypes.NameIdentifier} claims not found");

            if (int.TryParse(claim.Value, out int id))
            {
                return id;
            }
            throw new AuthenticationException($"Invalid claim {ClaimTypes.NameIdentifier} value: {claim.Value}");
        }

    }
}