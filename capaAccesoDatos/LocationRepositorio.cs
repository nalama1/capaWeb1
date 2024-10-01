using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace capaAccesoDatos
{
    public class LocationRepositorio
    {
        private string conexion_string;
        public LocationRepositorio()
        {
            conexion_string = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }

        public List<SelectListItem> cargarComboPaises()
        {
            List<SelectListItem> paises = new List<SelectListItem>();

            try
            {
                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerPaises", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                paises.Add(new SelectListItem
                                    {
                                        Value = reader["PaisID"].ToString(),
                                        Text = reader["Nombre"].ToString()
                                    });
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return paises;
        }

        public List<SelectListItem> cargarComboCiudadesXPais(int paisId)
        {
            List<SelectListItem> ciudades = new List<SelectListItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerCiudadesPorPais", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaisID", paisId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ciudades.Add(new SelectListItem
                                    {
                                        Value = reader["CiudadID"].ToString(),
                                        Text = reader["Nombre"].ToString()
                                    });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return ciudades;
        }
    }
}
