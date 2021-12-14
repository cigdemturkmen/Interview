using Interview.Data.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.UI.Models
{
    public class RequestViewModel
    {
        [DisplayName("Talep No")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [DisplayName("Soyisim")]
        public string Surname { get; set; }


        [DisplayName("Mesaj")]
        public string Message { get; set; }

        [DisplayName("Dosya")]
        [Required(ErrorMessage = "Zorunlu alan")]
        public IFormFile File { get; set; }
        public string FileStr { get; set; }

        [DisplayName("Kullanıcı Id")]
        public int? UserId { get; set; }

        [DisplayName("Olumlu mu?")]
        public bool IsPositive { get; set; }

        [DisplayName("Değerlendirildi mi?")]
        public bool IsEvaluated { get; set; }

       

        [DisplayName("Açıklama")]
        public string AdminMessage { get; set; }

        [DisplayName("Talep Tarihi")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Değerlendirilme Tarihi")]
        public DateTime? UpdatedDate { get; set; }


    }
}
