using Microsoft.Data.SqlClient;
using MvcCoreLinqToSql.Models;
using System.Data;

namespace MvcCoreLinqToSql.Repositories
{
    public class RepositoryEnfermos
    {
        private SqlConnection conn;
        private SqlCommand command;

        private DataTable tablaEnfermos;

        public RepositoryEnfermos()
        {
            string stringConn = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";

            conn = new SqlConnection(stringConn);
            command = new SqlCommand();

            string sql = "select * from ENFERMO";
            SqlDataAdapter ad = new SqlDataAdapter(sql, stringConn);
            tablaEnfermos = new DataTable();
            ad.Fill(tablaEnfermos);
        }

        private void InicializarCommand(string sql)
        {
            command.Connection = conn;
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
        }

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in tablaEnfermos.AsEnumerable()
                           select datos;
            List<Enfermo> enfermos = new List<Enfermo>();
            foreach (var fila in consulta)
            {
                Enfermo e = new Enfermo();
                e.Inscripcion = fila.Field<string>("INSCRIPCION");
                e.Apellido = fila.Field<string>("APELLIDO");
                e.Direccion = fila.Field<string>("DIRECCION");
                e.FechaNac = fila.Field<DateTime>("FECHA_NAC");
                e.Sexo = fila.Field<string>("S");
                e.Nss = fila.Field<string>("NSS");
                enfermos.Add(e);
            }

            return enfermos;
        }

        public Enfermo FindEnfermo(string inscripcion)
        {
            var consulta = from datos in tablaEnfermos.AsEnumerable()
                           where datos.Field<string>("INSCRIPCION") == inscripcion
                           select datos;
            var fila = consulta.First();

            Enfermo e = new Enfermo();
            e.Inscripcion = fila.Field<string>("INSCRIPCION");
            e.Apellido = fila.Field<string>("APELLIDO");
            e.Direccion = fila.Field<string>("DIRECCION");
            e.FechaNac = fila.Field<DateTime>("FECHA_NAC");
            e.Sexo = fila.Field<string>("S");
            e.Nss = fila.Field<string>("NSS");

            return e;
        }

        public int DeleteEnfermo(string inscripcion)
        {
            int registros = 0;

            string sql = "Delete from ENFERMO where INSCRIPCION = @ins";
            command.Parameters.Clear();
            SqlParameter paramIns = new SqlParameter("@ins", inscripcion);
            command.Parameters.Add(paramIns);

            InicializarCommand(sql);
            conn.Open();
            registros = command.ExecuteNonQuery();

            conn.Close();

            return registros;
        }
    }
}
