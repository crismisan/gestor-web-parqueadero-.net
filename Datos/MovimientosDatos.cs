using Parqueadero.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace Parqueadero.Datos
{
    public class MovimientoDatos
    {
      

       
        public List<MovimientosModel> ListarMovimientos()
        {
            var oLista = new List<MovimientosModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarMovimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new MovimientosModel()
                        {
                            Idmovimiento = Convert.ToInt32(dr["Idmovimiento"]),
                            FechaHora = Convert.ToDateTime(dr["FechaHora"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                            IdCupo = Convert.ToInt32(dr["IdCupo"]),
                            TipoMovimiento = Convert.ToString(dr["TipoMovimiento"]),
                            IdVehiculo = Convert.ToInt32(dr["IdVehiculo"])
                        });
                    }
                }
            }
            return oLista;
        }

        public MovimientosModel ObtenerMovimientos(int Idmovimiento)
        {
            var oMovimiento = new MovimientosModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarMovimiento", conexion);
                cmd.Parameters.AddWithValue("Idmovimiento", Idmovimiento);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oMovimiento.Idmovimiento = Convert.ToInt32(dr["Idmovimiento"]);
                        oMovimiento.FechaHora = Convert.ToDateTime(dr["FechaHora"]);
                        oMovimiento.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        oMovimiento.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]);
                        oMovimiento.IdCupo = Convert.ToInt32(dr["IdCupo"]);
                        oMovimiento.TipoMovimiento = Convert.ToString(dr["TipoMovimiento"]);
                        oMovimiento.IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]);
                    }
                }
            }
            return oMovimiento;
        }

        public bool GuardarMovimientos(MovimientosModel oMovimiento)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarMovimiento", conexion);
                    cmd.Parameters.AddWithValue("FechaHora", oMovimiento.FechaHora);
                    cmd.Parameters.AddWithValue("IdUsuario", oMovimiento.IdUsuario);
                    cmd.Parameters.AddWithValue("IdEmpleado", oMovimiento.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdCupo", oMovimiento.IdCupo);
                    cmd.Parameters.AddWithValue("TipoMovimiento", oMovimiento.TipoMovimiento);
                    cmd.Parameters.AddWithValue("IdVehiculo", oMovimiento.IdVehiculo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool EditarMovimientos(MovimientosModel oMovimiento)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarMovimiento", conexion);
                    cmd.Parameters.AddWithValue("Idmovimiento", oMovimiento.Idmovimiento);
                    cmd.Parameters.AddWithValue("FechaHora", oMovimiento.FechaHora);
                    cmd.Parameters.AddWithValue("IdUsuario", oMovimiento.IdUsuario);
                    cmd.Parameters.AddWithValue("IdEmpleado", oMovimiento.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdCupo", oMovimiento.IdCupo);
                    cmd.Parameters.AddWithValue("TipoMovimiento", oMovimiento.TipoMovimiento);
                    cmd.Parameters.AddWithValue("IdVehiculo", oMovimiento.IdVehiculo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool EliminarMovimientos(int Idmovimiento)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarMovimiento", conexion);
                    cmd.Parameters.AddWithValue("Idmovimiento", Idmovimiento);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }
        public List<MovimientosModel> ObtenerMovimientosPorParametros(ReportParametersModel parameters)
        {
            var lista = new List<MovimientosModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerMovimientosPorParametros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters.StartDate.HasValue)
                    cmd.Parameters.AddWithValue("@StartDate", parameters.StartDate.Value);
                if (parameters.EndDate.HasValue)
                    cmd.Parameters.AddWithValue("@EndDate", parameters.EndDate.Value);
                if (parameters.UserId.HasValue)
                    cmd.Parameters.AddWithValue("@UserId", parameters.UserId.Value);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new MovimientosModel
                        {
                            Idmovimiento = Convert.ToInt32(dr["IdMovimiento"]),
                            FechaHora = Convert.ToDateTime(dr["FechaHora"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                            IdCupo = Convert.ToInt32(dr["IdCupo"]),
                            TipoMovimiento = dr["TipoMovimiento"].ToString(),
                            IdVehiculo = Convert.ToInt32(dr["IdVehiculo"])
                        });
                    }
                }
            }

            return lista;
        }
    }
}
