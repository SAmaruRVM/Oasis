using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título tem que ser obrigatóriamente preenchido!")]
        [StringLength(50, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Título", Prompt = "Introduza o título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O conteúdo tem que ser obrigatóriamente preenchido!")]
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
