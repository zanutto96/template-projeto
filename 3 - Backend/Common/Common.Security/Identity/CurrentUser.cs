using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Common.Security.Identity
{

    public class CurrentUser : ICurrentUser
    {

        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.FindFirst("Name")?.Value;

        public Guid GetUserId()
        {
            return IsAutenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public int GetSubjectId()
        {
            return IsAutenticated() ? int.Parse(_accessor.HttpContext.User.FindFirst("SubjectId")?.Value) : default(int);
        }

        public int GetMembroId()
        {
            return IsAutenticated() ? int.Parse(_accessor.HttpContext.User.FindFirst("MembroId")?.Value) : default(int);
        }

        public string GetPerfil()
        {
            return IsAutenticated() ? _accessor.HttpContext.User.FindFirst("Perfil")?.Value : default(string);
        }

        public string GetUserEmail()
        {
            return IsAutenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public bool IsAutenticated()
        {
            return _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }

    }
}
