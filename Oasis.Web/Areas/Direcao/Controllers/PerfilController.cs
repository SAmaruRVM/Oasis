using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Direcao.Controllers {

    public class PerfilController : BaseDirecaoController
    {
        public IActionResult Index() {
            return View();
        }
    }
}
