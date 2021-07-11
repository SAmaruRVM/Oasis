using Microsoft.AspNetCore.Mvc;
namespace Oasis.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index() => View();
    }
}
