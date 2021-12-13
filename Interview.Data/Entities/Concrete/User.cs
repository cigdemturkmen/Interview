using Interview.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Interview.Data.Entities.Concrete
{
    public class User : BaseEntity
    {
        // e-posta, şifre, ad soyad ve cep telefonu

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }


        public UserType UserType { get; set; }

        public List<Request> Requests { get; set; }
    }
}
