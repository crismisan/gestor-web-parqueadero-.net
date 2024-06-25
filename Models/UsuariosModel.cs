using System.ComponentModel.DataAnnotations;
namespace Parqueadero.Models
{
    public class UsuariosModel
    {
        public int Idusuario {get; set; }
        
        [Required(ErrorMessage ="El campo Nombre es obligatorio")]
        public String? Nombre {get; set; }

        [Required (ErrorMessage ="El campo Correo es obligatorio")]
        public String? Email { get; set; }

        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public double Telefono { get; set; }

    }
}
