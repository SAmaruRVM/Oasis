using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public ICollection<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();

        public int ApplicationUserId { get; set; }
    }
}
