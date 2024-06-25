namespace Parqueadero.Models
{
    public class AsignacionModel
    {
        public int IdAsignacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCupo { get; set; }
        public int IdVehiculo { get; set; }
        public DateTime FechaHoraAsignacion { get; set; }
        public List<UsuariosModel> Usuarios { get; set; }
        public List<EmpleadoModel> Empleados { get; set; }
        public List<CuposModel> Cupos { get; set; }
        public List<VehiculosModel> Vehiculos { get; set; }
    }
}
