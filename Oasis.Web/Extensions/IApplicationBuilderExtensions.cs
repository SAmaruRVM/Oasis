using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace Oasis.Web.Extensions
{
    internal static class IApplicationBuilderExtensions
    {
        public static void Configurar(this IApplicationBuilder @this, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                @this.UseDeveloperExceptionPage();            
            }

            @this.UseRouting();

            @this.UseStaticFiles();
          
            @this.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: string.Empty,
                    pattern: "{controller=Home}/{action=Index}"
                );
            });
        }
    }
}
