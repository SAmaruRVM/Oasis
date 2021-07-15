using System;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class ComentarioPostUtilizador
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Comentario { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public Post Post { get; set; }

        public ApplicationUser Utilizador { get; set; }

        public int PostId { get; set; }

        public int ApplicationUserId { get; set; }
    }
}
