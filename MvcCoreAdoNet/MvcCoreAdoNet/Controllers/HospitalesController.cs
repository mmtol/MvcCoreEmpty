using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Repositories;
using MvcCoreAdoNet.Models;
using System.Threading.Tasks;

namespace MvcCoreAdoNet.Controllers
{
    public class HospitalesController : Controller
    {
        private RepositoryHospital repo;

        public HospitalesController()
        {
            this.repo = new RepositoryHospital();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Hospital> hospitales = await this.repo.GetHospitalesAsync();
            return View(hospitales);
        }
        public async Task<IActionResult> Details(int id)
        {
            Hospital hospital = await this.repo.FindHospitalAsync(id);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            Hospital hospital = await repo.FindHospitalAsync(id);
            return View(hospital);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteHospitalAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hospital hospital)
        {
            await this.repo.InsertHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono, hospital.Camas);
            ViewData["Mensaje"] = "Hospital insertado";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hospital hospital)
        {
            await this.repo.UpdateHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono, hospital.Camas);
            ViewData["Mensaje"] = "Hospital modificado";
            //return RedirectToAction("Index");
            return View();
        }
    }
}
