using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoMSK.Areas.Default.Models
{
    public class RegisterViewModel
    {
        public Guid ID { get; set; }
        public int ConfirmCode { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]

        [StringLength(160, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9_-]{3,15}$", ErrorMessage = "Логин введен не верно")]
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
    }
}