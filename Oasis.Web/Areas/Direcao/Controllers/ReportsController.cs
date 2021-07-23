using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Oasis.Web.Areas.Direcao.Controllers {

    public class ReportsController : BaseDirecaoController
    {
        [HttpGet]
        public ViewResult Index() => View();
    }
}
