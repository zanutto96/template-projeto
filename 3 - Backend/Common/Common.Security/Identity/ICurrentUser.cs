using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Common.Security.Identity
{

    public interface ICurrentUser
    {
        string Name { get; }
        Guid GetUserId();
        int GetSubjectId();
        string GetUserEmail();
        int GetMembroId();
        string GetPerfil();
        bool IsAutenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetUserClaims();
        HttpContext GetHttpContext();

    }
}
