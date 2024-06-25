using Microsoft.AspNetCore.Mvc;
using Parqueadero.Datos;
using Parqueadero.Models;

namespace Parqueadero.Controllers
{
    public class EmpleadoController : Controller
    {
        private EmpleadoDatos _empleadoDatos = new EmpleadoDatos();
        public IActionResult ListarEmpleados()
        {
            var oLista = _empleadoDatos.Listar();
            return View(oLista);
        }

        public IActionResult GuardarEmpleados() {

            return View();
        
        }

        [HttpPost]
        public IActionResult GuardarEmpleados(EmpleadoModel oEmpleado)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = _empleadoDatos.Guardar(oEmpleado);
            if (respuesta)
                return RedirectToAction("ListarEmpleados");
            else
                return View();
        }

        public IActionResult EditarEmpleados(int Idempleado)
        {

            var oempleado = _empleadoDatos.Obtener(Idempleado);

            return View(oempleado);
        }

        [HttpPost]
        public IActionResult EditarEmpleados(EmpleadoModel oempleado)
        {

            if (!ModelState.IsValid)
                return View();


            var respuesta = _empleadoDatos.Editar(oempleado);
            if (respuesta)
                return RedirectToAction("ListarEmpleados");
            else
                return View();
            return View();
        }

        public IActionResult EliminarEmpleados(int Idempleado)
        {

            var oempleado = _empleadoDatos.Obtener(Idempleado);

            return View(oempleado);
        }

        [HttpPost]
        public IActionResult EliminarEmpleados(EmpleadoModel oEmpleado)
        {

            var respuesta = _empleadoDatos.Eliminar(oEmpleado.Idempleado);
            if (respuesta)
                return RedirectToAction("ListarEmpleados");
            else
                return View();
            return View();
        }
    }

}

