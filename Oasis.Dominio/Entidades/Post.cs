using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        public string Conteudo { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public byte[] Ficheiro { get; set; }

        public TipoPost TipoPost { get; set; }

        public ApplicationUser Criador { get; set; }

        public int TipoPostId { get; set; }

        public int ApplicationUserId { get; set; }

        public ICollection<PostUtilizadorGuardado> UtilizadoresQueGuardaram { get; } = new List<PostUtilizadorGuardado>();
        public ICollection<PostGostoUtilizador> UtilizadoresQueGostaram { get; } = new List<PostGostoUtilizador>();
        public ICollection<ComentarioPostUtilizador> Comentarios { get; } = new List<ComentarioPostUtilizador>();
    }
}
