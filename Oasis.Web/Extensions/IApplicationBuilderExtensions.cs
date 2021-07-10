using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            @this.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
