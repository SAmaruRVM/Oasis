using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class GruposViewModel
    {
        public IEnumerable<Grupo> Grupos { get; set; }

        public Grupo GrupoAlterar { get; set; }

        public InsercaoParticipantesGrupoViewModel[] InsercaoParticipantesGrupoViewModels { get; set; }
        
        public AtualizarBannerViewModel AtualizarBannerViewModel { get; set; }
    }
}