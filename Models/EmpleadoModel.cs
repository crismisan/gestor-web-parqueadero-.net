using System.ComponentModel.DataAnnotations;
namespace Parqueadero.Models
{
    public class EmpleadoModel
    {
        public int Idempleado { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        public string Cargo { get; set; }
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string Email { get; set; }
    }
}
