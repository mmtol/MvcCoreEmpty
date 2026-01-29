using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Repositories;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryHospital repo;

        public DoctoresController()
        {
            repo = new RepositoryHospital();
        }

        [HttpGet]
        public async Task<IActionResult> DoctoresEspecialidad()
        {
            List<Doctor> doctores = await repo.GetDoctoresAsync();
            List<string> especialidades = await repo.GetEspecialidadesAsync();
            ViewData["ESPECIALIDADES"] = especialidades;

            return View(doctores);
        }

        [HttpPost]
        public async Task<IActionResult> DoctoresEspecialidad(string especialidad)
        {
            List<Doctor> doctores = await repo.GetDoctoresEspecialidadAsync(especialidad);
            List<string> especialidades = await repo.GetEspecialidadesAsync();
            ViewData["ESPECIALIDADES"] = especialidades;
            return View(doctores);
        }
    }
}
