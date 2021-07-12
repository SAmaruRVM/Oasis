using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Oasis.Aplicacao.Extensions
{
    public static class SmtpClientExtensions
    {
        public static async Task EnviarEmailAsync(this SmtpClient @this, string assunto, string conteudo, string emailDestinatario, Action<ConfiguracoesEmail> configuracoesEmail)
        {
            using (@this)
            {
                using MailMessage mailMessage = new();
                mailMessage.To.Add(emailDestinatario);
                ConfiguracoesEmail emailConfiguracoes = new();
                configuracoesEmail(emailConfiguracoes);
                @this.Credentials = emailConfiguracoes.Credenciais;
                @this.Port = emailConfiguracoes.SmtpPort;
                @this.Host = emailConfiguracoes.SmtpHost;
                @this.EnableSsl = true;
                mailMessage.From = new MailAddress(emailConfiguracoes.Email);
                mailMessage.Subject = assunto;
                mailMessage.Body = conteudo;
                await @this.SendMailAsync(mailMessage);
            }
        }
    }
}
