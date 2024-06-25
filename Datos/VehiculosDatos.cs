using Parqueadero.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Parqueadero.Datos
{
    public class VehiculosDatos
    {
        public List<VehiculosModel> Listar()
        {
            var oLista = new List<VehiculosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarVehiculos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new VehiculosModel()
                        {
                            Idvehiculos = Convert.ToInt32(dr["Idvehiculos"]),
                            Placa = Convert.ToString(dr["Placa"]),
                            Modelo = Convert.ToString(dr["Modelo"]),
                            UsuarioId = Convert.ToInt32(dr["UsuarioId"])

                        });
                    }
                }
            }
            return oLista;
        }
        public VehiculosModel Obtener(int Idvehiculos)
        {
            var oVehiculo = new VehiculosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarVehiculo", conexion);
                cmd.Parameters.AddWithValue("Idvehiculos", Idvehiculos);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oVehiculo.Idvehiculos = Convert.ToInt32(dr["Idvehiculos"]);
                        oVehiculo.Placa = Convert.ToString(dr["Placa"]);
                        oVehiculo.Modelo = Convert.ToString(dr["Modelo"]);
                        oVehiculo.UsuarioId = Convert.ToInt32(dr["UsuarioId"]);
                    }
                }
            }
            return oVehiculo;
        }

        public bool Guardar(VehiculosModel oVehiculo)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))

                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("Placa", oVehiculo.Placa);
                    cmd.Parameters.AddWithValue("Modelo", oVehiculo.Modelo);
                    cmd.Parameters.AddWithValue("UsuarioId", oVehiculo.UsuarioId);
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
        public bool EditarVehiculos(VehiculosModel oVehiculo)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("Idvehiculos", oVehiculo.Idvehiculos);
                    cmd.Parameters.AddWithValue("Placa", oVehiculo.Placa);
                    cmd.Parameters.AddWithValue("Modelo", oVehiculo.Modelo);
                    cmd.Parameters.AddWithValue("UsuarioId", oVehiculo.UsuarioId);
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
        public bool EliminarVehiculos(int Idvehiculos)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("Idvehiculos", Idvehiculos);
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
