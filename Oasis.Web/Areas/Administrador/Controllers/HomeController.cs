using Microsoft.AspNetCore.Mvc;
namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class HomeController : BaseAdministradorController
    {
        [HttpGet]
        public RedirectToActionResult Index() => RedirectToAction(actionName: "Index", controllerName: "Escolas", new { area = "Administrador"});
    }
}
