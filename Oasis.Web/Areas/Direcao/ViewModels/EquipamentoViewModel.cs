using Oasis.Dominio.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Direcao.ViewModels
{
    public class EquipamentoViewModel
    {
        public IEnumerable<Equipamento> Equipamentos { get; set; }

        public int StockEquipamentos { get; set; }
    }
}
