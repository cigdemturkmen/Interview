using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen email adresinizi giriniz.")]
        [EmailAddress(ErrorMessage = "ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "En az 6, en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }
    }
}
