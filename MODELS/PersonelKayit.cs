using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personel.Models
{
    public class PersonelKayit
    {
        public int id { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public bool Cinsiyet { get; set; }

        public int Ulke { get; set; }

        public int Sehir { get; set; }

        public string Aciklama { get; set; }

        public DateTime DogumTarihi { get; set; }

        public bool active { get; set; }

        [NotMapped] // NotMapped attribute prevents EF Core from trying to map this property to the database.
        public IFormFile ImageFile { get; set; }

        public byte[] Image { get; set; }

    }


}
