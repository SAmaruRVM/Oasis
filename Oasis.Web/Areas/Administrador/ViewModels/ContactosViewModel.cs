using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.Areas.Administrador.ViewModels
{
    public sealed class ContactosViewModel
    {
        public IEnumerable<Contacto> Contactos { get; set; }
        public RespostaContacto RespostaContactoAdicionar { get; set; }
    }
}