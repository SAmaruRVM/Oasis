using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Aluno.Controllers
{

    [Area("Alunos")]
    [Authorize(Roles = "Aluno")]
    public class BaseAlunoController : Controller
    {
    }
}
