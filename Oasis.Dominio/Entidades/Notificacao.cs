using System;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Notificacao
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }
        
        [Required]
        public string LinkDestino { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public bool FoiVista { get; set; }

        public ApplicationUser Utilizador { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
