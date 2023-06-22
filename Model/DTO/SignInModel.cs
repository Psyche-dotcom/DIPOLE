using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class SignInModel
    {
        [Required]
        public string AppId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string AppKey { get; set; }
    }
}
