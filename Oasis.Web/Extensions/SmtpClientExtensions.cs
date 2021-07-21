using Microsoft.Extensions.Configuration;
using Oasis.Aplicacao;
using System;
using System.Net;
using System.Net.Mail;

namespace Oasis.Web.Extensions
{
    internal static class SmtpClientExtensions
    {
        public static Action<ConfiguracoesEmail> ConfiguracoesEmail(this SmtpClient @this, IConfiguration configuration)
            => configuracoesEmail =>
            {
                configuracoesEmail.Credenciais = new NetworkCredential
                {
                    UserName = configuration["Projeto:Email:User"],
                    Password = configuration["Projeto:Email:Password"]
                };
                configuracoesEmail.Email = configuration["Projeto:Email:User"];
                configuracoesEmail.SmtpHost = configuration["Projeto:Email:Smtp-Host"];
                configuracoesEmail.SmtpPort = int.Parse(configuration["Projeto:Email:Smtp-Port"]);
            };
       
    }
}
