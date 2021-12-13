using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.UI.Models
{
    public class RegisterViewModel
    {
        // e-posta, şifre, ad soyad ve cep telefonu + validasyonlar

        [Required(ErrorMessage ="Bu alan zorunludur!")]
        [StringLength(50, ErrorMessage ="En fazla 50 karakter girilebilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter girilebilir.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [StringLength(20, ErrorMessage = "En fazla 20 karakter girilebilir.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [EmailAddress(ErrorMessage = "ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [StringLength(12, MinimumLength =6, ErrorMessage = "En az 6, en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrenizi tekrar giriniz.")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor!")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "En az 6, en fazla 12 karakter girilebilir.")]
        public string PasswordConfirm { get; set; }
 
    }
}
