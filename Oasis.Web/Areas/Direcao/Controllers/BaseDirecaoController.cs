using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Oasis.Web.Areas.Direcao.Controllers
{
    [Area("Direcao")]
    [Authorize(Roles = "Diretor")]
    public class BaseDirecaoController : Controller
    {
    }
}
