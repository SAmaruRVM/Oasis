using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título tem que ser obrigatóriamente preenchido!")]
        [StringLength(50, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Título", Prompt = "Introduza o título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O título tem que ser obrigatóriamente preenchido!")]
        [Display(Name = "Conteúdo", Prompt = "O que gostavas de partilhar com os teus colegas?")]
        public string Conteudo { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public byte[] Ficheiro { get; set; }

        public TipoPost TipoPost { get; set; }

        public ApplicationUser Criador { get; set; }

        public int TipoPostId { get; set; }

        public int ApplicationUserId { get; set; }

        [JsonIgnore]
        public ICollection<PostUtilizadorGuardado> UtilizadoresQueGuardaram { get; } = new List<PostUtilizadorGuardado>();

        [JsonIgnore]
        public ICollection<PostGostoUtilizador> UtilizadoresQueGostaram { get; } = new List<PostGostoUtilizador>();

        [JsonIgnore]
        public ICollection<ComentarioPostUtilizador> Comentarios { get; } = new List<ComentarioPostUtilizador>();

        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }
    }
}
