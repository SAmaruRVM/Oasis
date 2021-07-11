using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oasis.Web.Extensions;
namespace Oasis.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => (_configuration) = (configuration);

        public void ConfigureServices(IServiceCollection services) => services.Adicionar(_configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.Configurar(env);
    }
}
