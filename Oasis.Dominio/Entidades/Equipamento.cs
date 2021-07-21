using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Equipamento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        // default 0 <<-- SQL
        public int Quantidade { get; set; }

        public Escola Escola { get; set; }

        public int EscolaId { get; set; }

        public ApplicationUser DirectorResponsavelInsercao { get; set; }

        public int ApplicationUserId { get; set; }

        public ICollection<RequisicaoEquipamento> Requisicoes { get; } = new List<RequisicaoEquipamento>();
    }
}
