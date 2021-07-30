using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Oasis.Web.ViewModels
{
    public class AtualizarBannerViewModel
    {
        public int IdGrupo { get; set; }


        [Display(Name = "A imagem de banner do grupo selecionado")]
        public IFormFile ImagemBanner { get; set; }
    }
}