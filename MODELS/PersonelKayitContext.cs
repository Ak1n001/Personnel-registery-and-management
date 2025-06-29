using Microsoft.EntityFrameworkCore;

namespace Personel.Models
{
    public class PersonelKayitContext : DbContext
    {
        public PersonelKayitContext(DbContextOptions<PersonelKayitContext> options) : base(options)
        {
            
        }
        public DbSet<PersonelKayit> PersonelKayit { get; set; }
        public DbSet<sehir_ulke_table> sehir_ulke_table { get; set; }
    }
}
