using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
namespace Oasis.Web.Extensions
{
    internal static class IApplicationBuilderExtensions
    {
        public static void Configurar(this IApplicationBuilder @this, IWebHostEnvironment env)
        {
            @this.UseDeveloperExceptionPage();

            @this.UseRouting();

            @this.UseStaticFiles();

            @this.UseAuthentication();
            @this.UseAuthorization();
            @this.UseSession();

            

            @this.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: string.Empty,
                     pattern: "{area:exists}/{controller=Home}/{action=Index}"
                 );

                endpoints.MapControllerRoute(
                    name: string.Empty,
                    pattern: "{controller=Home}/{action=Index}"
                );
            });
        }
    }
}
