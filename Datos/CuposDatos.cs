using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Parqueadero.Models;
namespace Parqueadero.Datos
{
    public class CuposDatos
    {
        public List<CuposModel> Listar()
        {
            var lista = new List<CuposModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarCupos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new CuposModel
                        {
                            Idcupo = Convert.ToInt32(dr["Idcupo"]),
                            Numero = Convert.ToInt32(dr["Numero"]),
                            Estado = dr["Estado"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public bool Insertar(CuposModel oCupos)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarCupo", conexion);
                    cmd.Parameters.AddWithValue("Numero", oCupos.Numero);
                    cmd.Parameters.AddWithValue("Estado", oCupos.Estado);
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

        public CuposModel Consultar(int idCupo)
        {
            var oCupo = new CuposModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarCupo", conexion);
                cmd.Parameters.AddWithValue("Idcupo", idCupo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCupo.Idcupo = Convert.ToInt32(dr["Idcupo"]);
                        oCupo.Numero = Convert.ToInt32(dr["Numero"]);
                        oCupo.Estado = dr["Estado"].ToString();
                    }
                }
            }

            return oCupo;
        }

        public bool Modificar(CuposModel oCupo)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarCupo", conexion);
                    cmd.Parameters.AddWithValue("Idcupo", oCupo.Idcupo);
                    cmd.Parameters.AddWithValue("Numero", oCupo.Numero);
                    cmd.Parameters.AddWithValue("Estado", oCupo.Estado);
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

        public bool Eliminar(int idCupo)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarCupo", conexion);
                    cmd.Parameters.AddWithValue("Idcupo", idCupo);
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
    

    

