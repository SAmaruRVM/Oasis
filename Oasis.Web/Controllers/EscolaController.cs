using Microsoft.AspNetCore.Mvc;
using System;

namespace Oasis.Web.Controllers
{
    public class EscolaController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
