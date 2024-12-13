using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(64)]
        public string Firstname { get; set; }
        [Required, MaxLength(64)]
        public string Lastname { get; set; }
        [Required, MaxLength(128), EmailAddress]
        public string Username { get; set; }
        [Required, MaxLength(128), EmailAddress]
        public string EmailAdress { get; set; }
        [Required, MaxLength(16), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(16), DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
