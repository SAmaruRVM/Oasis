using Microsoft.AspNetCore.Mvc.Rendering;
using Oasis.Dominio.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.ViewModels
{
    public class PerfilUtilizadorViewModel
    {
        public IEnumerable<ApplicationUser> UtilizadoresEscola { get; set; }

        public int TemasId { get; set; }
        public IEnumerable<SelectListItem> TemasDropDown { get; set; }
    }
}
