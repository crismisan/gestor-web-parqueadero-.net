using System.Data.SqlClient;
using System.Data;
using Parqueadero.Models;
using System.Data.SqlClient;
using System.Data;
namespace Parqueadero.Datos
{
    public class EntregaDatos
    {
        public List<EntregaModel> Listar()
        {
            var lista = new List<EntregaModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarEntregasParqueadero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new EntregaModel
                        {
                            IdEntrega = Convert.ToInt32(dr["IdEntrega"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                            IdCupo = Convert.ToInt32(dr["IdCupo"]),
                            IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                            FechaHoraEntrega = Convert.ToDateTime(dr["FechaHoraEntrega"])
                        });
                    }
                }
            }

            return lista;
        }
        public bool InsertarEntrega(EntregaModel oEntrega)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarEntregaParqueadero", conexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oEntrega.IdUsuario);
                    cmd.Parameters.AddWithValue("IdEmpleado", oEntrega.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdAsignacion", oEntrega.IdAsignacion);
                    cmd.Parameters.AddWithValue("IdCupo", oEntrega.IdCupo);
                    cmd.Parameters.AddWithValue("IdVehiculo", oEntrega.IdVehiculo);
                    cmd.Parameters.AddWithValue("FechaHoraEntrega", oEntrega.FechaHoraEntrega);
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

        public EntregaModel Consultar(int idEntrega)
        {
            var oEntrega = new EntregaModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultarEntregaParqueadero", conexion);
                cmd.Parameters.AddWithValue("IdEntrega", idEntrega);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oEntrega.IdEntrega = Convert.ToInt32(dr["IdEntrega"]);
                        oEntrega.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        oEntrega.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]);
                        oEntrega.IdCupo = Convert.ToInt32(dr["IdCupo"]);
                        oEntrega.IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]);
                        oEntrega.FechaHoraEntrega = Convert.ToDateTime(dr["FechaHoraEntrega"]);
                    }
                }
            }

            return oEntrega;
        }

        public bool Modificar(EntregaModel oEntrega)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarEntregaParqueadero", conexion);
                    cmd.Parameters.AddWithValue("IdEntrega", oEntrega.IdEntrega);
                    cmd.Parameters.AddWithValue("IdUsuario", oEntrega.IdUsuario);
                    cmd.Parameters.AddWithValue("IdEmpleado", oEntrega.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdCupo", oEntrega.IdCupo);
                    cmd.Parameters.AddWithValue("IdVehiculo", oEntrega.IdVehiculo);
                    cmd.Parameters.AddWithValue("FechaHoraEntrega", oEntrega.FechaHoraEntrega);
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

        public bool Eliminar(int idEntrega)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarEntregaParqueadero", conexion);
                    cmd.Parameters.AddWithValue("IdEntrega", idEntrega);
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

        public AsignacionModel ObtenerDatosAsignacionPorId(int idAsignacion)
        {
            AsignacionModel asignacion = null;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerDatosAsignacion", conexion);
                    cmd.Parameters.AddWithValue("IdAsignacion", idAsignacion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            asignacion = new AsignacionModel
                            {
                                IdAsignacion = Convert.ToInt32(dr["IdAsignacion"]),
                                // Llenar las demás propiedades según lo que retorne tu procedimiento almacenado
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                // Manejo del error
            }

            return asignacion;
        }
    }
}

