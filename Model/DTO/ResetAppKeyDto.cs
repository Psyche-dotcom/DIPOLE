using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class ResetAppKeyDto
    {
       
        public string AppKey { get; set; }
        [Compare("AppKey", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmAppKey { get; set; }
        public string AppId { get; set; }
        public string Token { get; set; }
    }
}
