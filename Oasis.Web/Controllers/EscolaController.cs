using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using System;

namespace Oasis.Web.Controllers
{
    [Route("[controller]")]
    public class EscolaController : Controller
    {
        private readonly OasisContext _context;
        public EscolaController(OasisContext context) => (_context) = (context);

        [HttpGet]
        [Route("[action]/{nomeEscola}")]
        [Route("{nomeEscola}")]
        
        public IActionResult Index(string nomeEscola) => View();
    }
}
