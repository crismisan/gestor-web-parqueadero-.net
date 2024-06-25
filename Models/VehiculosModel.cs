using System.ComponentModel.DataAnnotations;
namespace Parqueadero.Models
{
        public class VehiculosModel
        {
            public int Idvehiculos { get; set; }
            public string Placa { get; set; }
            public string Modelo { get; set; }
            public int UsuarioId { get; set; }

            public List<UsuariosModel> Usuarios { get; set; }   
        }

    }

