using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Equipamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(50, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Nome", Prompt = "Introduza o nome")]
        public string Nome { get; set; }

        // default 0 <<-- SQL
        [Required(ErrorMessage = "A quantidade tem que ser obrigatóriamente preenchida!")]
        [Display(Name = "Quantidade", Prompt = "Introduza a quantidade")]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O código tem que ser obrigatóriamente preenchido!")]
        [StringLength(50, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Codigo", Prompt = "Introduza o código para o equipamento")]
        public string codigoEquipamento { get; set; }

        public DateTime? DataSaida { get; set; }

        public DateTime DataEntrada { get; set; }

        public Escola Escola { get; set; }

        public int EscolaId { get; set; }

        public ApplicationUser DirectorResponsavelInsercao { get; set; }

        public int ApplicationUserId { get; set; }

        public RequisicaoEquipamento Requisicao { get; }

        public int? RequisicaoEquipamentoId { get; set; }
    }
}
