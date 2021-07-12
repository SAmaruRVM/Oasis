using System.Net;


namespace Oasis.Aplicacao
{
    public sealed class ConfiguracoesEmail
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string Email { get; set; }
        public NetworkCredential Credenciais { get; set; }
    }
}
