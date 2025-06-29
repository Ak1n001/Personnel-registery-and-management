using System.Collections.Generic;

namespace Personel.Models
{
    public class Personel_Ulke
    {
        public List<PersonelKayit> personelKayits { get; set; } 

        public List<sehir_ulke_table> ulkeSehirTable { get; set; }  
    }
}
