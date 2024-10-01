using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaAccesoDatos
{
    public class OrdenCompraRepositorio
    {
        private readonly string conexion_string;
        public OrdenCompraRepositorio()
        {
            conexion_string = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }

        public bool grabarOrdenCompra(List<CarritoItem> carrito, int UsuarioID, string direccionEntrega)
        {
            bool bandera;
            string q1 = "insert into Ordenes (UsuarioID, Fecha, Total, Estatus, DireccionEntrega) " +
                        "output inserted.OrdenID" + //esto es para el ExecuteScalar()
                        "values (@UsuarioID, @Fecha, @Total, @Estatus, @DireccionEntrega)";
            string q2 = "insert into OrdenDetalles (OrdenID, ProductoID, Cantidad, PrecioUnitario, Total) " +
                        "values (@OrdenID, @ProductoID, @Cantidad, @PrecioUnitario, @Total)";
            string q3 = "update Producto" + 
                        "set CantidadDisponible = CantidadDisponible - @cantidadSeleccionada" +
                        "where ProductoID = @ProductoID" ;
            try
            {
                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {                            
                            SqlCommand cmd1 = new SqlCommand(q1, con, transaction);
                            cmd1.Parameters.AddWithValue("@UsuarioID", UsuarioID);
                            cmd1.Parameters.AddWithValue("@Fecha", DateTime.Now);
                            cmd1.Parameters.AddWithValue("@Total", carrito.Sum(x=> x.Total));
                            cmd1.Parameters.AddWithValue("@Estatus", "En proceso");
                            cmd1.Parameters.AddWithValue("@DireccionEntrega", direccionEntrega);
                            int OrdenID = (int)cmd1.ExecuteScalar();
                            
                            foreach (var item in carrito)
                            {   SqlCommand cmd2 = new SqlCommand(q2, con, transaction);
                                cmd2.Parameters.AddWithValue("@OrdenID", OrdenID);
                                cmd2.Parameters.AddWithValue("@ProductoID", item.Producto.ProductoID);
                                cmd2.Parameters.AddWithValue("@Cantidad", item.CantidadSeleccionada);
                                cmd2.Parameters.AddWithValue("@PrecioUnitario", (decimal) item.Producto.Precio);
                                cmd2.Parameters.AddWithValue("@Total", (decimal) item.Total);
                                int filasAfectadas = (int) cmd2.ExecuteNonQuery();

                                SqlCommand cmd3 = new SqlCommand(q3, con, transaction);
                                cmd3.Parameters.AddWithValue("@cantidadSeleccionada", item.CantidadSeleccionada);
                                cmd3.Parameters.AddWithValue("@ProductoID", item.Producto.ProductoID);
                                cmd3.ExecuteNonQuery();
                            }
                            bandera = true;
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            bandera = false;
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return bandera;
        }
    }
//    OrdenID	int
//UsuarioID	int
//Fecha	datetime
//Total	decimal
//Estatus	varchar
//DireccionEntrega	varchar



    //OrdenDetalleID	int
    //OrdenID	int
    //ProductoID	int
    //Cantidad	int
    //PrecioUnitario	decimal
    //Total	decimal
}

//ProductoID	int Producto
//Nombre	nvarchar
//Descripcion	nvarchar
//Precio	decimal
//CantidadDisponible	int
