using System;

namespace Oasis.Dominio.Entidades
{
    public class RequisicaoEquipamento
    {
        public int Id { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataRequisicao { get; set; }

        public DateTime DataEntrega { get; set; }

        public bool EstaAprovado { get; set; }

        public ApplicationUser Aluno { get; set; }
        public Equipamento Equipamento { get; set; }

        public int ApplicationUserId { get; set; }
        public int EquipamentoId { get; set; }
    }
}
