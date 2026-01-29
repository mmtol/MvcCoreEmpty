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
            int num = 99;

            Empleado empleado = repo.FindEmpleado(id);
            return View(empleado);
        }
    }
}
