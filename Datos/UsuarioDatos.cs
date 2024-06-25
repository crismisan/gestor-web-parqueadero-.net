using Parqueadero.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Parqueadero.Datos
{
    public class UsuarioDatos
    {
        public List<UsuariosModel> Listar()
        {
            var oLista= new List<UsuariosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new UsuariosModel() {
                            Idusuario = Convert.ToInt32(dr["Idusuario"]),
                            Nombre = Convert.ToString(dr["Nombre"]),
                            Email = Convert.ToString(dr["Email"]),
                            Telefono = Convert.ToDouble(dr["Telefono"])
                            
                        });
                    }
                }
            }
            return oLista;
        }

        public UsuariosModel Obtener(int Idusuario)
        {
            var oUsuario = new UsuariosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarUsuario", conexion);
                cmd.Parameters.AddWithValue("Idusuario", Idusuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oUsuario.Idusuario = Convert.ToInt32(dr["Idusuario"]);
                        oUsuario.Nombre = Convert.ToString(dr["Nombre"]);
                        oUsuario.Telefono = Convert.ToDouble(dr["Telefono"]);
                        oUsuario.Email = Convert.ToString(dr["Email"]);
                    }
                }
            }
            return oUsuario;
        }

        public bool Guardar(UsuariosModel oUsuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))

                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", conexion);
                    cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                    cmd.Parameters.AddWithValue("Email", oUsuario.Email);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta= true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;

        }

        public bool EditarUsuarios(UsuariosModel oUsuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarUsuario", conexion);
                    cmd.Parameters.AddWithValue("Idusuario", oUsuario.Idusuario);
                    cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                    cmd.Parameters.AddWithValue("Email", oUsuario.Email);
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

        public bool EliminarUsuarios(int Idusuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conexion);
                    cmd.Parameters.AddWithValue("Idusuario", Idusuario);
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

        public bool ProbarConexion()
        {
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                try
                {
                    conexion.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }


    }
}