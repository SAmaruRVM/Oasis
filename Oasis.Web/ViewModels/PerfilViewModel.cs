using System.ComponentModel.DataAnnotations;

namespace Oasis.Web.ViewModels
{
    public class PerfilViewModel
    {
        [StringLength(200, ErrorMessage = "A descrição do seu perfil {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Descrição do seu perfil", Prompt = "Introduza a sua descrição. Como por exemplo, um pequeno excerto que permita aos outros utilizadores conhecerem-lhe um pouco melhor.")]
        public string Descricao { get; set; }
        public byte[] ImagemPerfil { get; set; }
        public AlterarPasswordViewModel AlterarPasswordViewModel { get; set; }
        public EscolaViewModel EscolaViewModel { get; set; }
        
        

    }
}