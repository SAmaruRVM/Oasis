using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Extensions;
using System.Threading.Tasks;

namespace Oasis.Web
{
    public class UserRazorPage<TModel> : RazorPage<TModel>
    {
        [RazorInject]
        public OasisContext DatabaseContext { get; set; }

        private ApplicationUser _utilizadorLogado = default;

        public async Task<ApplicationUser> GetUtilizadorLogadoAsync()
        {
            if (_utilizadorLogado is not null) 
            {
                return _utilizadorLogado;
            }

            return _utilizadorLogado = await DatabaseContext.GetLoggedInApplicationUser(User.Identity.Name);
        }
      




        public override Task ExecuteAsync() => throw new System.NotImplementedException();

    }
}
