using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;
using System.Data;
using System.Globalization;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryHospital
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryHospital()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            cn = new SqlConnection(connectionString);
            com = new SqlCommand();
            com.Connection = this.cn;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            string sql = "select * from HOSPITAL";
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            reader = await com.ExecuteReaderAsync();
            List<Hospital> hospitales = new List<Hospital>();
            while (await reader.ReadAsync())
            {
                Hospital h = new Hospital();
                h.IdHospital = int.Parse(reader["HOSPITAL_COD"].ToString());
                h.Nombre = reader["NOMBRE"].ToString();
                h.Direccion = reader["DIRECCION"].ToString();
                h.Telefono = reader["TELEFONO"].ToString();
                h.Camas = int.Parse(reader["NUM_CAMA"].ToString());
                hospitales.Add(h);
            }
            await reader.CloseAsync();
            await cn.CloseAsync();
            return hospitales;
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            string sql =
                "select * from HOSPITAL where HOSPITAL_COD=@hospitalcod";
            com.Parameters.AddWithValue("@hospitalcod", idHospital);
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            reader = await com.ExecuteReaderAsync();
            Hospital hospital = new Hospital();
            await reader.ReadAsync();
            hospital.IdHospital =
                int.Parse(reader["HOSPITAL_COD"].ToString());
            hospital.Nombre = reader["NOMBRE"].ToString();
            hospital.Direccion = reader["DIRECCION"].ToString();
            hospital.Telefono = reader["TELEFONO"].ToString();
            hospital.Camas =
                int.Parse(reader["NUM_CAMA"].ToString());
            await reader.CloseAsync();
            await cn.CloseAsync();
            com.Parameters.Clear();
            return hospital;
        }

        public async Task InsertHospitalAsync
            (int idHospital, string nombre, string direccion
            , string telefono, int camas)
        {
            string sql = "insert into HOSPITAL values (@hospitalcod, "
                + "@nombre, @direccion, @telefono,@camas)";
            com.Parameters.AddWithValue("@hospitalcod", idHospital);
            com.Parameters.AddWithValue("@nombre", nombre);
            com.Parameters.AddWithValue("@direccion", direccion);
            com.Parameters.AddWithValue("@telefono", telefono);
            com.Parameters.AddWithValue("@camas", camas);
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            await com.ExecuteNonQueryAsync();
            await cn.CloseAsync();
            com.Parameters.Clear();
        }

        public async Task UpdateHospitalAsync(int idHospital, string nombre
            , string direccion, string telefono, int camas)
        {
            string sql = "update HOSPITAL set NOMBRE=@nombre, "
                + "DIRECCION=@direccion, TELEFONO=@telefono, "
                + "NUM_CAMA=@camas where HOSPITAL_COD=@hospitalcod";
            com.Parameters.AddWithValue("@nombre", nombre);
            com.Parameters.AddWithValue("@direccion", direccion);
            com.Parameters.AddWithValue("@telefono", telefono);
            com.Parameters.AddWithValue("@camas", camas);
            com.Parameters.AddWithValue("@hospitalcod", idHospital);
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            await com.ExecuteNonQueryAsync();
            await cn.CloseAsync();
            com.Parameters.Clear();
        }

        public async Task DeleteHospitalAsync(int idHospital)
        {
            string sql = "delete from HOSPITAL where HOSPITAL_COD=@hospitalcod";
            com.Parameters.AddWithValue("@hospitalcod", idHospital);
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            await com.ExecuteNonQueryAsync();
            await cn.CloseAsync();
            com.Parameters.Clear();
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string sql = "select * from DOCTOR";
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            reader = await com.ExecuteReaderAsync();
            List<Doctor> doctores = new List<Doctor>();
            while (await reader.ReadAsync())
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = int.Parse(reader["DOCTOR_NO"].ToString());
                doc.Apeliido = reader["APELLIDO"].ToString();
                doc.Especialidad = reader["ESPECIALIDAD"].ToString();
                doc.Salario = int.Parse(reader["SALARIO"].ToString());
                doc.IdHospital = int.Parse(reader["HOSPITAL_COD"].ToString());

                doctores.Add(doc);
            }

            await reader.CloseAsync();
            await cn.CloseAsync();
            return doctores;
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(String especialidad)
        {
            string sql = "select * from DOCTOR where ESPECIALIDAD=@especialidad";
            SqlParameter paramEspe = new SqlParameter("@especialidad", especialidad);
            com.Parameters.Add(paramEspe);
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            reader = await com.ExecuteReaderAsync();
            List<Doctor> doctores = new List<Doctor>();
            while (await reader.ReadAsync())
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = int.Parse(reader["DOCTOR_NO"].ToString());
                doc.Apeliido = reader["APELLIDO"].ToString();
                doc.Especialidad = reader["ESPECIALIDAD"].ToString();
                doc.Salario = int.Parse(reader["SALARIO"].ToString());
                doc.IdHospital = int.Parse(reader["HOSPITAL_COD"].ToString());

                doctores.Add(doc);
            }

            await reader.CloseAsync();
            await cn.CloseAsync();
            return doctores;
        }

        public async Task<List<string>> GetEspecialidadesAsync()
        {
            string sql = "select distinct ESPECIALIDAD from DOCTOR";
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            await cn.OpenAsync();
            reader = await com.ExecuteReaderAsync();
            List<string> especialidades = new List<string>();
            while (await reader.ReadAsync())
            {
                string espe = reader["ESPECIALIDAD"].ToString();
                especialidades.Add(espe);
            }

            await reader.CloseAsync();
            await cn.CloseAsync();

            return especialidades;
        }
    }
}