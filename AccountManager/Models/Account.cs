using System.ComponentModel.DataAnnotations;

namespace AccountManager.Models
{
    public class Account
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required.")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; } = DateTime.MinValue;

        public string Birthplace { get; set; } = string.Empty;

        [Required(ErrorMessage = "Residence is required.")]
        public string Residence { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
