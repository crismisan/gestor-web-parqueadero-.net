using Parqueadero.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Parqueadero.Datos
{
    public class EmpleadoDatos
    {
        public List<EmpleadoModel> Listar()
        {
            var lista = new List<EmpleadoModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new EmpleadoModel
                        {
                            Idempleado = Convert.ToInt32(dr["Idempleado"]),
                            Nombre = dr["Nombre"].ToString(),
                            Cargo = dr["Cargo"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Email = dr["Email"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public bool Guardar(EmpleadoModel oEmpleado)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarEmpleado", conexion);
                    cmd.Parameters.AddWithValue("Nombre", oEmpleado.Nombre);
                    cmd.Parameters.AddWithValue("Cargo", oEmpleado.Cargo);
                    cmd.Parameters.AddWithValue("Telefono", oEmpleado.Telefono);
                    cmd.Parameters.AddWithValue("Email", oEmpleado.Email);
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

        public EmpleadoModel Obtener(int idEmpleado)
        {
            var oEmpleado = new EmpleadoModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarEmpleado", conexion);
                cmd.Parameters.AddWithValue("Idempleado", idEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oEmpleado.Idempleado = Convert.ToInt32(dr["Idempleado"]);
                        oEmpleado.Nombre = dr["Nombre"].ToString();
                        oEmpleado.Cargo = dr["Cargo"].ToString();
                        oEmpleado.Telefono = dr["Telefono"].ToString();
                        oEmpleado.Email = dr["Email"].ToString();
                    }
                }
            }

            return oEmpleado;
        }

        public bool Editar(EmpleadoModel oEmpleado)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarEmpleado", conexion);
                    cmd.Parameters.AddWithValue("Idempleado", oEmpleado.Idempleado);
                    cmd.Parameters.AddWithValue("Nombre", oEmpleado.Nombre);
                    cmd.Parameters.AddWithValue("Cargo", oEmpleado.Cargo);
                    cmd.Parameters.AddWithValue("Telefono", oEmpleado.Telefono);
                    cmd.Parameters.AddWithValue("Email", oEmpleado.Email);
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

        public bool Eliminar(int idEmpleado)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", conexion);
                    cmd.Parameters.AddWithValue("Idempleado", idEmpleado);
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
