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

        public Users ConsultarUsuarioEspecifico(string usuario, string clave)
        {
            Users user = null;
            string q1 = "select UserId, Username, PasswordHash, Email, FullName from Users where Username = @Username and PasswordHash = @clave";
            try
            {
                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(q1, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);
                        cmd.Parameters.AddWithValue("@clave", clave);
                        using (SqlDataReader reader = cmd.ExecuteReader()) //el cursor executa el query
                        {
                            if (reader.Read()) // es crucial esto, para acceder a la 1era fila del cursor y leer la data
                            {
                                user = new Users
                                {
                                    UserId = (int)reader["UserId"],
                                    Username = reader["Username"].ToString(),
                                    PasswordHash = reader["PasswordHash"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    FullName = reader["FullName"].ToString()
                                };
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

            return user;
            //
        }

    }
}
