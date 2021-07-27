using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Aluno.Controllers
{
    public class ApoioController : BaseAlunoController
    {
        private readonly OasisContext _context;

        public ApoioController(OasisContext context) => _context = (context);
        
        [HttpGet]
        public ViewResult Index() => View();
    }
}
