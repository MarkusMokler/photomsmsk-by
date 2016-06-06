using System;
using System.ComponentModel.DataAnnotations;


namespace PhotoMSK.Areas.Default.Models
{
    public class ResetPasswordModel
    {
        public Guid ID { get; set; }
        public int ConfirmCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Пароль долен быть более {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}