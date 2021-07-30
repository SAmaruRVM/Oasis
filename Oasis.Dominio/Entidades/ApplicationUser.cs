using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class ApplicationUser : IdentityUser<int>
    {
        public byte[] ImagemPerfil { get; set; }

        [StringLength(200, ErrorMessage = "A descrição do seu perfil {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Descrição do seu perfil", Prompt = "Introduza a sua descrição. Como por exemplo, um pequeno excerto que permita aos outros utilizadores conhecerem-lhe um pouco melhor.")]
        public string DescricaoPerfil { get; set; }

        [Required(ErrorMessage = "O primeiro nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O primeiro nome {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Primeiro Nome", Prompt = "Introduza o primeiro nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O apelido tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O apelido {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Apelido", Prompt = "Introduza o apelido")]
        public string Apelido { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        // se for nulo, significa que ainda não realizou o login
        public DateTime? DataUltimoLogin { get; set; }

        // Tema Escolhido
        public Tema Tema { get; set; }
        public int TemaId { get; set; }


        public Escola Escola { get; set; }
        public int? EscolaId { get; set; }

        [JsonIgnore]
        public ICollection<Disciplina> DisciplinasCriadas { get; } = new List<Disciplina>();

        [JsonIgnore]
        public ICollection<Equipamento> EquipamentosInseridos { get; set; } = new List<Equipamento>();

        [JsonIgnore]
        public ICollection<Grupo> GruposOndeEnsina { get; } = new List<Grupo>();

        [JsonIgnore]
        public ICollection<GrupoAluno> GruposOndeTemAulas { get; } = new List<GrupoAluno>();

        [JsonIgnore]
        public ICollection<Post> PostsCriados { get; } = new List<Post>();

        [JsonIgnore]
        public ICollection<PostUtilizadorGuardado> PostsGuardados { get; } = new List<PostUtilizadorGuardado>();

        [JsonIgnore]
        public ICollection<PostGostoUtilizador> PostsGostados { get; } = new List<PostGostoUtilizador>();

      

        [JsonIgnore]
        public ICollection<Notificacao> Notificacoes { get; } = new List<Notificacao>();

        [JsonIgnore]
        public ICollection<RequisicaoEquipamento> RequisicoesEquipamento { get; } = new List<RequisicaoEquipamento>();

        [JsonIgnore]
        public ICollection<ComentarioPostUtilizador> Comentarios { get; } = new List<ComentarioPostUtilizador>();
    }
}
