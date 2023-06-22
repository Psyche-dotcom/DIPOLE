using Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class SignUp
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string AppKey { get; set; }
        [Required]
        [Compare("AppKey")]
        [DataType(DataType.Password)]
        public string ConfirmAppKey { get; set; }
        public Gender Gender { get; set; }
    }
}
