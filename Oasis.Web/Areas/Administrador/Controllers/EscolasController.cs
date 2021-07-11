using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class EscolasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
