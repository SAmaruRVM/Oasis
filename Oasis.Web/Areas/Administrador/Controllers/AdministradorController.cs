using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers {

    [Area("Administrador")]
    public class AdministradorController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
