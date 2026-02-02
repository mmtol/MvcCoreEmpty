using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToSql.Repositories;
using MvcCoreLinqToSql.Models;

namespace MvcCoreLinqToSql.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;

        public EnfermosController()
        {
            repo = new RepositoryEnfermos();
        }

        public IActionResult Index()
        {
            List<Enfermo> enfermos = repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(string inscripcion)
        {
            Enfermo enfermo = repo.FindEnfermo(inscripcion);
            return View(enfermo);
        }

        public IActionResult Delete(string inscripcion)
        {
            repo.DeleteEnfermo(inscripcion);
            return RedirectToAction("Index");
        }
    }
}
