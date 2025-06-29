using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personel.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Personel.Controllers
{
    public class EditController : Controller
    {
        private readonly ILogger<EditController> _logger;

        private readonly PersonelKayitContext _DbPersonelKayitContext;


        public EditController(ILogger<EditController> logger, PersonelKayitContext context)
        {
            _logger = logger;
            _DbPersonelKayitContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public Personel_Ulke Update(int id)
        {
            List<PersonelKayit> employees = _DbPersonelKayitContext.PersonelKayit.ToList();
            List<sehir_ulke_table> tablo = _DbPersonelKayitContext.sehir_ulke_table.ToList();

            Personel_Ulke personelUlke = new Personel_Ulke();
            personelUlke.ulkeSehirTable = tablo;
            personelUlke.personelKayits = employees;

            return personelUlke;
        }

        public List<sehir_ulke_table> UlkeGetir()
        {
            List<sehir_ulke_table> ulkesehirlist = new List<sehir_ulke_table>();
            //List<PersonelKayit> personelKayits = _DbPersonelKayitContext.PersonelKayit.ToList();
            ulkesehirlist = _DbPersonelKayitContext.sehir_ulke_table.Where(u => u.ust_id == 0).ToList();
            return ulkesehirlist;
        }

        [Route("/Edit/SehirGetir/{selectedCountryId}")]

        public List<sehir_ulke_table> SehirGetir(int selectedCountryId)
        {
            List<sehir_ulke_table> sehirList = new List<sehir_ulke_table>();
            //List<PersonelKayit> personelKayits = _DbPersonelKayitContext.PersonelKayit.ToList();
            sehirList = _DbPersonelKayitContext.sehir_ulke_table.Where(u => u.ust_id == selectedCountryId).ToList();
            return sehirList;
        }

        [HttpPost]
        public IActionResult InsertUserDataUpdated(PersonelKayit employee, int idgelen1)
        {
            PersonelKayit employeetoUpdate = _DbPersonelKayitContext.PersonelKayit.FirstOrDefault(x => x.id == employee.id);

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
            using (var memoryStream = new MemoryStream())
            {
                employee.ImageFile.CopyTo(memoryStream);
                employee.Image = memoryStream.ToArray();
            }

            // Set the "Active" status to false
            employeetoUpdate.Ad = employee.Ad;
            employeetoUpdate.Soyad = employee.Soyad;
            employeetoUpdate.Aciklama = employee.Aciklama;
            employeetoUpdate.Ulke = employee.Ulke;
            employeetoUpdate.Sehir = employee.Sehir;
            employeetoUpdate.Cinsiyet = employee.Cinsiyet;
            employeetoUpdate.DogumTarihi = employee.DogumTarihi;
            employeetoUpdate.Image=employee.Image;

           

            // Save changes to the database
            _DbPersonelKayitContext.SaveChanges();

            return Ok(new { message = "Employee deactivated successfully" });
        }
    }
}
