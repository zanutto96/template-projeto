namespace Common.Domain
{
    public class JwtSettingsBase
    {
        public string Secret { get; set; }
        public string Salt { get; set; }
    }
}