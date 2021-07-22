using System;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Notificacao
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public bool FoiVista { get; set; }

        public ApplicationUser Utilizador { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
