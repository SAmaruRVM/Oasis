using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oasis.Dados;

namespace Oasis.Web.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void Adicionar(this IServiceCollection @this, IConfiguration configuration) 
        {
            @this.AddControllersWithViews();

            @this.AddDbContextPool<OasisContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("SQL-SERVER"))
            );

            @this.AddRouting(options => 
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });
        }
    }
}
