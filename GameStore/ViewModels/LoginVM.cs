using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class LoginVM
    {
        [Required, MaxLength(128), EmailAddress]
        public string EmailAdress { get; set; }
        [Required, MaxLength(16), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
