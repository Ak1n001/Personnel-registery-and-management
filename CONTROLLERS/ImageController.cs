using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personel.Models;

namespace Personel.Controllers
{
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;

        private readonly PersonelKayitContext _DbPersonelKayitContext;


        public ImageController(ILogger<ImageController> logger, PersonelKayitContext context)
        {
            _logger = logger;
            _DbPersonelKayitContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
