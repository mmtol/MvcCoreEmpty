using Microsoft.Data.SqlClient;
using MvcCoreLinqToSql.Models;
using System.Data;

namespace MvcCoreLinqToSql.Repositories
{
    public class RepositoryEmpleados
    {
        //solo tendremos una tabla a nivel de clase para nuestras consultas
        private DataTable tablaEmpleados;

        public RepositoryEmpleados()
        {
            string stringConn = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from EMP";
            //creamos el adaptador puente entre sql server y linq
            SqlDataAdapter ad = new SqlDataAdapter(sql, stringConn);
            tablaEmpleados = new DataTable();
            //taremos los datos para linq
            ad.Fill(tablaEmpleados);
        }

        //metodo para recuperar todos los empleados
        public List<Empleado> GetEmpleados()
        {
            //las consultas se almacenan en genericos
            var consulta = from datos in tablaEmpleados.AsEnumerable()
                           select datos;
            //ahora mismo tenemos dentro de consulra la informacion de los empleados
            //los datos vienen en formato tabla y cada elemento de una tabla
            //es una fila (DataRow)
            //debemos recorrer las filas, extraerlas y convertirlas a nuestro model
            List<Empleado> empleados = new List<Empleado>();
            //recorremos cada fila de la consulta
            foreach (var fila in consulta)
            {
                //para extraer datos de un datarow
                //DataRow.Field<TIPO>(NOMBRE_COLUMNA)
                Empleado emp = new Empleado();
                emp.IdEmpleado = fila.Field<int>("EMP_NO");
                emp.Apellido = fila.Field<string>("APELLIDO");
                emp.Oficio = fila.Field<string>("OFICIO");
                emp.Salario = fila.Field<int>("SALARIO");
                emp.IdDepartamento = fila.Field<int>("DEPT_NO");
                empleados.Add(emp);
            }

            return empleados;
        }

        public Empleado FindEmpleado(int idEmpleado)
        {
            //filtramos nuestra consulta
            var consulta = from datos in tablaEmpleados.AsEnumerable()
                           where datos.Field<int>("EMP_NO") == idEmpleado
                           select datos;
            //nosotros sabemos que esta consulta devuelve una fila
            //pero linq siempr devuelve un conjunto
            //dentro de este conjunto tenemos metodos lambda
            //por ejemplo, podriamos contar, saber el max
            //o recuperar el primer elemento del conjunto
            var fila = consulta.First();

            Empleado emp = new Empleado();
            emp.IdEmpleado = fila.Field<int>("EMP_NO");
            emp.Apellido = fila.Field<string>("APELLIDO");
            emp.Oficio = fila.Field<string>("OFICIO");
            emp.Salario = fila.Field<int>("SALARIO");
            emp.IdDepartamento = fila.Field<int>("DEPT_NO");

            return emp;
        }

        public List<Empleado> GetEmpleadoOficioSalario(string oficio, int salario)
        {
            var consulta = from datos in tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio
                           && datos.Field<int>("SALARIO") >= salario
                           select datos;
            if (consulta.Count() == 0)
            {
                //siempre devolvemos null
                return null;
            }
            else
            {
                List<Empleado> emplados = new List<Empleado>();
                foreach (var fila in consulta)
                {
                    Empleado e = new Empleado
                    {
                        IdEmpleado = fila.Field<int>("EMP_NO"),
                        Apellido = fila.Field<string>("APELLIDO"),
                        Oficio = fila.Field<string>("OFICIO"),
                        Salario = fila.Field<int>("SALARIO"),
                        IdDepartamento = fila.Field<int>("DEPT_NO")
                    };

                    emplados.Add(e);
                }

                return emplados;
            }
        }
    }
}
