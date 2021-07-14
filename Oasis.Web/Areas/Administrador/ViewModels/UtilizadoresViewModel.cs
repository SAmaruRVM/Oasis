using Microsoft.AspNetCore.Mvc.Rendering;
using Oasis.Dominio.Entidades;
using System.Collections.Generic;

namespace Oasis.Web.Areas.Administrador.ViewModels
{
    public sealed class UtilizadoresViewModel
    {
        public ApplicationUser MembroDirecao { get; set; }
        public int IdEscola { get; set; }
        public IEnumerable<ApplicationUser> Utilizadores { get; set; }
        public IEnumerable<SelectListItem> EscolasDropdownList { get; set; }
    }
}
