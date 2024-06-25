using Microsoft.AspNetCore.Mvc;
using Parqueadero.Datos;
using Parqueadero.Models;
namespace Parqueadero.Controllers
{
    public class CuposController : Controller
    {
        CuposDatos _Cuposdatos = new CuposDatos();
        public IActionResult ListarCupos()
        {
            var oLista = _Cuposdatos.Listar();
            return View(oLista);
        }

        public IActionResult GuardarCupos() { 
        return View();
        }

        [HttpPost]
        public IActionResult GuardarCupos(CuposModel oCupos)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = _Cuposdatos.Insertar(oCupos);
            if (respuesta)
                return RedirectToAction("ListarCupos");
            else
                return View();
        }

        public IActionResult EditarCupos(int Idcupo) // debe llamarse igual que como se declara en el boton
        {
            /*al darle editar en listar, se envia el id a este metodo, este metodo lo envia al metodo 
             de obtener id para tomar todos los datos del usuario, se guardan todos los atributos del usuario
            en ousuario y esos datos se envian a la vista, osea editarusuarios, llegan a la vista
            y mediante el model se ponen en los input con los nombres exactos y por eso se llenan automaticamente
            en el formulario*/
            var ocupos = _Cuposdatos.Consultar(Idcupo);

            return View(ocupos);
        }

        [HttpPost]
        public IActionResult EditarCupos(CuposModel ocupos)
        {

            if (!ModelState.IsValid)
                return View();


            var respuesta = _Cuposdatos.Modificar(ocupos);
            if (respuesta)
                return RedirectToAction("ListarCupos");
            else
                return View();
            return View();
        }

        public IActionResult EliminarCupos(int Idcupo)
        {

            var ocupos = _Cuposdatos.Consultar(Idcupo);

            return View(ocupos);
        }

        [HttpPost]  
        public IActionResult EliminarCupos(CuposModel oCupos)
        {

            var respuesta = _Cuposdatos.Eliminar(oCupos.Idcupo);
            if (respuesta)
                return RedirectToAction("ListarCupos");
            else
                return View();
            return View();
        }
    }
}

