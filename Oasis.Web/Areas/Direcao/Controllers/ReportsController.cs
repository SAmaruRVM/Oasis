using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Oasis.Web.Areas.Direcao.Controllers {

    public class ReportsController : BaseDirecaoController
    {
        public ViewResult Index() => View();
    }
}
