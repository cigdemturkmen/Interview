using Interview.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Interview.Data.Entities.Concrete
{
    public class Request : BaseEntity
    {
        // Kullanıcı giriş yaptıktan sonra yeni bir talep oluşturacaktır.Bu talepte ad soyad (zorunlu), açıklama (seçimli) ve belge ekleme (zorunlu) olacaktır.

        //Kullanıcı yaptığı talepleri bir liste şeklinde görecek ve talep değerlendirmesinin değerlendirme zamanı ve (olumlu/olumsuz) değerlendirme sonucunu görecektir.

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(1000)]
        public string AdminMessage { get; set; }

        [Required]
        public byte[] File { get; set; }


        public int? UserId { get; set; }
        public User User { get; set; }

        public bool IsPositive { get; set; }

        public bool IsEvaluated { get; set; }
    }
}
