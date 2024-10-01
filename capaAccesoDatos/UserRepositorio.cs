using Entidades;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace capaAccesoDatos
{
    public class UserRepositorio
    {
        private string conexion_string;
        public UserRepositorio()
        {
            conexion_string = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }

        public bool RegisterUser(Users user)
        {             
            try
            {
                if (user == null) throw new ArgumentNullException(); /////////////////////////////////

                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("RegisterUser", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@PaisID", user.PaisID);
                        cmd.Parameters.AddWithValue("@CiudadID", user.CiudadID);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0; /////////////////////////////////
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
