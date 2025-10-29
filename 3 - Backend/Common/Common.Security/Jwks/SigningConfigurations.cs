using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common.Security.Jwks
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(string _key)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
