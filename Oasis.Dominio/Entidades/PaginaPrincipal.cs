using System;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class PaginaPrincipal
    {
        public int Id { get; set; }

        [DataType(DataType.Html)]
        public string ConteudoHtml { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        // se for nulo, significa que a página ainda não foi alterada desde a sua criação
        public DateTime? DataUltimaAlteracao { get; set; }

        public Escola Escola { get; set; }
    }
}
