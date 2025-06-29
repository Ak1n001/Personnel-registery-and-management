using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Personel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Personel.Controllers
{
    public class ListController : Controller
    {
        private readonly ILogger<ListController> _logger;

        private readonly PersonelKayitContext _DbPersonelKayitContext;




        public ListController(ILogger<ListController> logger, PersonelKayitContext context)
        {
            _logger = logger;
            _DbPersonelKayitContext = context;
        }





        public IActionResult List()
        {
            return View();
        }

       

        [HttpGet]
        public Personel_Ulke GetEmployees()
        {
            List<PersonelKayit> employees = _DbPersonelKayitContext.PersonelKayit.ToList();
            List<sehir_ulke_table> tablo = _DbPersonelKayitContext.sehir_ulke_table.ToList();

            Personel_Ulke personelUlke = new Personel_Ulke();
            personelUlke.ulkeSehirTable = tablo;
            personelUlke.personelKayits = employees;

            return personelUlke;
        }

    
        [HttpPost]
        public IActionResult DeactivateEmployee(int id)
        {
            PersonelKayit employee = _DbPersonelKayitContext.PersonelKayit.FirstOrDefault(x => x.id == id);

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            // Set the "Active" status to false
            employee.active = false;

            // Save changes to the database
            _DbPersonelKayitContext.SaveChanges();

            return Ok(new { message = "Employee deactivated successfully" });
        }





    }
}
