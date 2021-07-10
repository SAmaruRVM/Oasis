using Microsoft.AspNetCore.Mvc;

namespace Oasis.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
