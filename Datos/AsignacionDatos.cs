using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Parqueadero.Models;

namespace Parqueadero.Datos
{
    public class AsignacionDatos
    {
        public List<AsignacionModel> Listar()
        {
            var lista = new List<AsignacionModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarAsignacionesParqueadero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new AsignacionModel
                        {
                            IdAsignacion = Convert.ToInt32(dr["IdAsignacion"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                            IdCupo = Convert.ToInt32(dr["IdCupo"]),
                            IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                            FechaHoraAsignacion = Convert.ToDateTime(dr["FechaHoraAsignacion"])
                        });
                    }
                }
            }

            return lista;
        }
        public bool Insertar(AsignacionModel oAsignacion)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarAsignacionParqueadero", conexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oAsignacion.IdUsuario);
                    cmd.Parameters.AddWithValue("IdEmpleado", oAsignacion.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdCupo", oAsignacion.IdCupo);
                    cmd.Parameters.AddWithValue("IdVehiculo", oAsignacion.IdVehiculo);
                    cmd.Parameters.AddWithValue("FechaHoraAsignacion", oAsignacion.FechaHoraAsignacion);
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
        public AsignacionModel Consultar(int idAsignacion)
        {
            var oAsignacion = new AsignacionModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarAsignacionParqueadero", conexion);
                cmd.Parameters.AddWithValue("IdAsignacion", idAsignacion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oAsignacion.IdAsignacion = Convert.ToInt32(dr["IdAsignacion"]);
                        oAsignacion.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        oAsignacion.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]);
                        oAsignacion.IdCupo = Convert.ToInt32(dr["IdCupo"]);
                        oAsignacion.IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]);
                        oAsignacion.FechaHoraAsignacion = Convert.ToDateTime(dr["FechaHoraAsignacion"]);
                    }
                }
            }

            return oAsignacion;
        }
        public bool Modificar(AsignacionModel oAsignacion)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarAsignacionParqueadero", conexion);
                    cmd.Parameters.AddWithValue("IdAsignacion", oAsignacion.IdAsignacion);
                    cmd.Parameters.AddWithValue("IdUsuario", oAsignacion.IdUsuario);
                    cmd.Parameters.AddWithValue("IdEmpleado", oAsignacion.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdCupo", oAsignacion.IdCupo);
                    cmd.Parameters.AddWithValue("IdVehiculo", oAsignacion.IdVehiculo);
                    cmd.Parameters.AddWithValue("FechaHoraAsignacion", oAsignacion.FechaHoraAsignacion);
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
        public bool Eliminar(int idAsignacion)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarAsignacionParqueadero", conexion);
                    cmd.Parameters.AddWithValue("IdAsignacion", idAsignacion);
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
    }
}

