using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;

namespace Oasis.Web.Areas.Direcao.Controllers {

    public class ReportsController : BaseDirecaoController
    {

        private readonly OasisContext _context;
        public ReportsController(OasisContext context) => (_context) = (context);


        [HttpGet]
        public ViewResult Index() => View();



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Criar([FromForm] int n) => Json(string.Empty);

        
    }
}
