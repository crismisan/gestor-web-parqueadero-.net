namespace Parqueadero.Models
{
    public class EntregaModel
    {
        public int IdEntrega { get; set; }
        public int IdAsignacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCupo { get; set; }
        public int IdVehiculo { get; set; }
        public DateTime FechaHoraEntrega { get; set; }

        // Propiedades para la asignación (datos que se llenarán automáticamente)
        public List<UsuariosModel> Usuarios { get; set; }
        public List<EmpleadoModel> Empleados { get; set; }
        public List<CuposModel> Cupos { get; set; }
        public List<VehiculosModel> Vehiculos { get; set; }

        public IEnumerable<AsignacionModel> AsignacionesDisponibles { get; set; }
    }
}
