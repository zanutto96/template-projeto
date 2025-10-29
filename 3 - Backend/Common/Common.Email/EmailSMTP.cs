using System.Net;
using Common.Domain;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Common.Email
{
    public class EmailSMTP : IDisposable
    {
        private IOptions<EmailConfigBase> _emailConfig;
        public EmailSMTP(
           IOptions<EmailConfigBase> emailConfig
           )
        {
            this._emailConfig = emailConfig;
        }

        public SendEmailAux Send(string EmailDestino, string Assunto, string Mensagem)
        {

            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(this._emailConfig.Value.NameFrom, this._emailConfig.Value.EmailFrom));
                mimeMessage.To.Add(new MailboxAddress(EmailDestino, EmailDestino));
                mimeMessage.Bcc.Add(new MailboxAddress("sistema@redekai.com.br", "sistema@redekai.com.br"));

                mimeMessage.Subject = Assunto;

                mimeMessage.Body = new TextPart(TextFormat.Html.ToString())
                {
                    Text = Mensagem
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(this._emailConfig.Value.SmtpServer, this._emailConfig.Value.SmtpPortNumber, false);
                    client.Authenticate(this._emailConfig.Value.SmtpUser, this._emailConfig.Value.SmtpPassword);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }

                return new SendEmailAux();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}