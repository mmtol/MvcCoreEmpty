namespace MvcCoreAdoNet.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public string Apeliido { get; set; }
        public string Especialidad { get; set; }
        public int Salario { get; set; }
        public int IdHospital { get; set; }
    }
}
