using Microsoft.AspNetCore.Mvc;
namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class HomeController : BaseAdministradorController
    {
        [HttpGet]
        public ViewResult Index() => View();
    }
}
