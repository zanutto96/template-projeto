namespace Common.Domain
{
    public class EmailConfigBase
    {
        public string EmailFrom { get; set; }
        public string NameFrom { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPortNumber { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
    }
}