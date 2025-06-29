using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using Personel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Personel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PersonelKayitContext  _DbPersonelKayitContext;



        public HomeController(ILogger<HomeController> logger, PersonelKayitContext context)
        {
            _logger = logger;
            _DbPersonelKayitContext = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Anasayfa()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<sehir_ulke_table> UlkeGetir()
        {
            List<sehir_ulke_table> ulkesehirlist = new List<sehir_ulke_table>();
            //List<PersonelKayit> personelKayits = _DbPersonelKayitContext.PersonelKayit.ToList();
            ulkesehirlist = _DbPersonelKayitContext.sehir_ulke_table.Where(u => u.ust_id == 0).ToList();
            return ulkesehirlist;
        }

        [Route("/Home/SehirGetir/{selectedCountryId}")]

        public List<sehir_ulke_table> SehirGetir(int selectedCountryId)
        {
            List<sehir_ulke_table> sehirList = new List<sehir_ulke_table>();
            //List<PersonelKayit> personelKayits = _DbPersonelKayitContext.PersonelKayit.ToList();
            sehirList = _DbPersonelKayitContext.sehir_ulke_table.Where(u => u.ust_id == selectedCountryId).ToList();
            return sehirList;
        }

        [HttpPost]
        public IActionResult InsertUserData(PersonelKayit userData)
        {
            if (userData != null)
            {
                // Check if an image file was provided
                if (userData.ImageFile != null)
                {
                    // Read the image bytes and store them in the Image property
                    using (var memoryStream = new MemoryStream())
                    {
                        userData.ImageFile.CopyTo(memoryStream);
                        userData.Image = memoryStream.ToArray();
                    }
                }

                userData.active = true;
                _DbPersonelKayitContext.PersonelKayit.Add(userData);
                _DbPersonelKayitContext.SaveChanges();

                return RedirectToAction("Index"); // You can redirect to a different action after insertion.
            }
            return View(userData);
        }


        public IActionResult GetImage(int id)
        {
            var user = _DbPersonelKayitContext.PersonelKayit.FirstOrDefault(u => u.id == id);

            if (user != null && user.Image != null)
            {
                // Assuming your image is stored as a byte array in the "Image" property of the "PersonelKayit" model
                byte[] imageBytes = user.Image;

                // Determine the content type of the image (e.g., image/jpeg, image/png, etc.)
                string contentType = "image/jpeg"; // Change this based on your image format

                // Return the image bytes as a FileContentResult with the appropriate content type
                return File(imageBytes, contentType);
            }

            // If the user or image doesn't exist, return an error message
            return Content("Image not found");
        }


    }

}
