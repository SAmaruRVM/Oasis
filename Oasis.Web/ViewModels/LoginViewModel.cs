using System.ComponentModel.DataAnnotations;

namespace Oasis.Web.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool LembrarDeMim { get; set; }
    }
}
