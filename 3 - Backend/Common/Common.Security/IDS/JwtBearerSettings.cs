namespace Common.Security.IdentityServer
{
    public class JwtBearerSettings
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}