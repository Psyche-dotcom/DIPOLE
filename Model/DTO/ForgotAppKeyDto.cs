using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ForgotAppKeyDto
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public ForgotAppKeyDto(string email, string resetToken)
        {
            Email = email;
            ResetToken = resetToken;
             
        }
    }
}
