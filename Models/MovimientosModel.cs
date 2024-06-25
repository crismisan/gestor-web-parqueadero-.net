using System.ComponentModel.DataAnnotations;
namespace Parqueadero.Models
{
    public class MovimientosModel
    {
        public int Idmovimiento { get; set; }
        public DateTime FechaHora { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCupo { get; set; }
        public string TipoMovimiento { get; set; }
        public int IdVehiculo { get; set; }
        public List<UsuariosModel> Usuarios { get; set; }
        public List<EmpleadoModel> Empleados { get; set; }
        public List<CuposModel> Cupos { get; set; }
        public List<VehiculosModel> Vehiculos { get; set; }
    }
}
