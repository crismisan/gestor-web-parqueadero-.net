using Microsoft.AspNetCore.Mvc;
using Parqueadero.Datos;
using Parqueadero.Models;
using System.Collections.Generic;

namespace Parqueadero.Controllers
{
    public class VehiculosController : Controller
    {

        VehiculosDatos _vehiculosDatos = new VehiculosDatos();
        UsuarioDatos _usuariosDatos = new UsuarioDatos();
        public IActionResult Listar()
        {
            var oLista = _vehiculosDatos.Listar();
            return View(oLista);
        }

        public IActionResult GuardarVehiculos()
        {
            var model = new VehiculosModel
            {
                Usuarios = _usuariosDatos.Listar()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GuardarVehiculos(VehiculosModel oVehiculos)
        {
            /*if (!ModelState.IsValid)
            {
                oVehiculos.Usuarios = _usuariosDatos.Listar();
            }
                return View(oVehiculos);
            */
            var respuesta = _vehiculosDatos.Guardar(oVehiculos);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult EditarVehiculos(int Idvehiculo) //Idvehiculo, porque asi lo declare en la vista asp-route-Idvehiculo
        {
            var oVehiculo = _vehiculosDatos.Obtener(Idvehiculo);
            return View(oVehiculo);
        }

        [HttpPost]
        public IActionResult EditarVehiculos(VehiculosModel oVehiculos)
        {
         /*   if (!ModelState.IsValid)
                return View();*/

            var Respuesta = _vehiculosDatos.EditarVehiculos(oVehiculos);
            if (Respuesta)
                return RedirectToAction("Listar");
            else return View();
            return View();
        }

        public IActionResult EliminarVehiculos(int Idvehiculo) // Idvehiculo, porque asi lo declare en la vista asp-route-Idvehiculo
        {
            var oVehiculos = _vehiculosDatos.Obtener(Idvehiculo);
            return View(oVehiculos);
        }

        [HttpPost]
        public IActionResult EliminarVehiculos(VehiculosModel oVehiculo) 
        {
            var respuesta = _vehiculosDatos.EliminarVehiculos(oVehiculo.Idvehiculos);
            if (respuesta)
                return RedirectToAction("Listar");
            else return View();
            return View();
        }
    }
}
