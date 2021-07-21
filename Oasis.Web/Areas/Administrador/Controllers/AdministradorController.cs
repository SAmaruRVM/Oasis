using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Oasis.Web.Areas.Administrador.Controllers {

    
    public class AdministradorController : BaseAdministradorController
    {
        public IActionResult Index() {
            return View();
        }
    }
}
