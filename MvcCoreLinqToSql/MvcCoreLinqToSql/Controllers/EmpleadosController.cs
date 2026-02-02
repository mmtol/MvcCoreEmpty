using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToSql.Repositories;
using MvcCoreLinqToSql.Models;

namespace MvcCoreLinqToSql.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController()
        {
            repo = new RepositoryEmpleados();
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            Empleado empleado = repo.FindEmpleado(id);
            return View(empleado);
        }

        public IActionResult BuscadorEmpleados()
        {
            return View();
        }

        public IActionResult DatosEmpleados()
        {
            List<string> oficios = repo.GetOficios();
            ViewData["Oficios"] = oficios;
            return View();
        }

        [HttpPost]
        public IActionResult BuscadorEmpleados(string oficio, int salario)
        {
            List<Empleado> emps = repo.GetEmpleadoOficioSalario(oficio, salario);

            if (emps == null)
            {
                ViewData["Mensaje"] = "No existen empleados con el oficio '" + oficio + "' y salario mayor a '" + salario + "'";
            }

            return View(emps);
        }

        [HttpPost]
        public IActionResult DatosEmpleados(string oficio)
        {
            ResumenEmpleados resume = repo.GetEmpleadosOficio(oficio);
            if (resume == null)
            {
                ViewData["Mensaje"] = "No existe ese oficio";
            }
            List<string> oficios = repo.GetOficios();
            ViewData["Oficios"] = oficios;
            ViewData["Oficio"] = oficio;
            return View(resume);
        }
    }
}
