using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class ApplicationUser : IdentityUser<int>
    {
        public byte[] ImagemPerfil { get; set; }

        [StringLength(200)]
        public string DescricaoPerfil { get; set; }

        [Required]
        [StringLength(20)]
        public string PrimeiroNome { get; set; }

        [Required]
        [StringLength(20)]
        public string Apelido { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        // se for nulo, significa que ainda não realizou o login
        public DateTime? DataUltimoLogin { get; set; }

        // Tema Escolhido
        public Tema Tema { get; set; }
        public int TemaId { get; set; }


        public ICollection<DirecaoEscola> EscolasEmQueFazParteDaDirecao { get; } = new List<DirecaoEscola>();
        public ICollection<Disciplina> DisciplinasCriadas { get; } = new List<Disciplina>();
        public ICollection<Grupo> GruposOndeEnsina { get; }  = new List<Grupo>();
        public ICollection<GrupoAluno> GruposOndeTemAulas { get; }  = new List<GrupoAluno>();
        public ICollection<Post> PostsCriados { get; }  = new List<Post>();
        public ICollection<PostUtilizadorGuardado> PostsGuardados { get; } = new List<PostUtilizadorGuardado>();
        public ICollection<PostGostoUtilizador> PostsGostados { get; } = new List<PostGostoUtilizador>();
        public ICollection<Report> ReportsProfessor { get; } = new List<Report>();
        public ICollection<Report> ReportsAluno { get; } = new List<Report>();
        public ICollection<Notificacao> Notificacoes { get; } = new List<Notificacao>();
        public ICollection<RequisicaoEquipamento> RequisicoesEquipamento { get; } = new List<RequisicaoEquipamento>();
        public ICollection<ComentarioPostUtilizador> Comentarios { get; } = new List<ComentarioPostUtilizador>();
    }
}
