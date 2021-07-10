using System.ComponentModel.DataAnnotations;
namespace Oasis.Dominio.Entidades
{
    public class RespostaContacto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Resposta { get; set; }

        public Contacto Contacto { get; set; }

        public int ContactoId { get; set; }
    }
}