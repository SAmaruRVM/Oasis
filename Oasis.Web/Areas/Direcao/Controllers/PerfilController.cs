using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Direcao.Controllers {
    [Area("Direcao")]
    public class PerfilController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
