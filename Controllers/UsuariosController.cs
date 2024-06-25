using Microsoft.AspNetCore.Mvc;
using Parqueadero.Datos;
using Parqueadero.Models;

namespace Parqueadero.Controllers
{
    public class UsuariosController : Controller
    {
        UsuarioDatos _UsuarioDatos = new UsuarioDatos();

        public IActionResult ListarUsuarios()
        {
            //La vista mostrará una lista de contactos
            var oLista = _UsuarioDatos.Listar();

            return View(oLista);
        }

        public IActionResult GuardarUsuarios()
        {
            //METODO SOLO DEVUELVE VISTA
            return View();
        }

        [HttpPost]
        public IActionResult GuardarUsuarios(UsuariosModel oUsuario)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if(!ModelState.IsValid)
                return View();  


            var respuesta = _UsuarioDatos.Guardar(oUsuario);
            if (respuesta)
                return RedirectToAction("ListarUsuarios");
            else 
                return View();
        }

        public IActionResult EditarUsuarios(int Idusuario)
        {
            /*al darle editar en listar, se envia el id a este metodo, este metodo lo envia al metodo 
             de obtener id para tomar todos los datos del usuario, se guardan todos los atributos del usuario
            en ousuario y esos datos se envian a la vista, osea editarusuarios, llegan a la vista
            y mediante el model se ponen en los input con los nombres exactos y por eso se llenan automaticamente
            en el formulario*/
            var ousuario = _UsuarioDatos.Obtener(Idusuario);

            return View(ousuario);
        }

        [HttpPost]
        public IActionResult EditarUsuarios(UsuariosModel oUsuario)     
        {
          
            if (!ModelState.IsValid)
                return View();


            var respuesta = _UsuarioDatos.EditarUsuarios(oUsuario);
            if (respuesta)
                return RedirectToAction("ListarUsuarios");
            else
                return View();
            return View();
        }

        public IActionResult EliminarUsuarios(int Idusuario)
        {
            
            var ousuario = _UsuarioDatos.Obtener(Idusuario);

            return View(ousuario);
        }

        [HttpPost]
        public IActionResult EliminarUsuarios(UsuariosModel oUsuario)
        {

            var respuesta = _UsuarioDatos.EliminarUsuarios(oUsuario.Idusuario);
            if (respuesta)
                return RedirectToAction("ListarUsuarios");
            else
                return View();
            return View();
        }
    }




}
