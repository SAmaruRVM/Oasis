using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Oasis.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    // [Authorize(Roles = "Administrador")]
    public class BaseAdministradorController : Controller
    {
    }
}
