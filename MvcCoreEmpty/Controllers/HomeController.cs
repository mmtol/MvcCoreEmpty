using Microsoft.AspNetCore.Mvc;

namespace MvcCoreEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vista1()
        {
            return View();
        }

        public IActionResult Vista2()
        {
            return View();
        }

        public IActionResult VistaPersona()
        {
            Persona persona = new Persona();
            persona.Nombre = "Pepa";
            persona.Email = "pepa@email.com";
            persona.Edad = 20;

            return View(persona);
        }
    }
}
