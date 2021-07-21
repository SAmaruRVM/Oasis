using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int EscolaId { get; set; }

        public ICollection<Disciplina> DisciplinasCriadas { get; } = new List<Disciplina>();
        public ICollection<Equipamento> EquipamentosInseridos { get; set; } = new List<Equipamento>();
        public ICollection<Grupo> GruposOndeEnsina { get; } = new List<Grupo>();
        public ICollection<GrupoAluno> GruposOndeTemAulas { get; } = new List<GrupoAluno>();
        public ICollection<Post> PostsCriados { get; } = new List<Post>();
        public ICollection<PostUtilizadorGuardado> PostsGuardados { get; } = new List<PostUtilizadorGuardado>();
        public ICollection<PostGostoUtilizador> PostsGostados { get; } = new List<PostGostoUtilizador>();
        public ICollection<Report> ReportsProfessor { get; } = new List<Report>();
        public ICollection<Report> ReportsAluno { get; } = new List<Report>();
        public ICollection<Notificacao> Notificacoes { get; } = new List<Notificacao>();
        public ICollection<RequisicaoEquipamento> RequisicoesEquipamento { get; } = new List<RequisicaoEquipamento>();
        public ICollection<ComentarioPostUtilizador> Comentarios { get; } = new List<ComentarioPostUtilizador>();
    }
}
