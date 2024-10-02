using Entidades;
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
    public class ProductoRepositorio
    {
        private string conexion_string;
        public ProductoRepositorio()
        {
            conexion_string = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }

        public List<Producto> ObtenerProductos() ////////// para View (Listado de Productos), View(productos)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerProductos", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
	                        {
                                Producto producto = new Producto
                                {
                                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                    Nombre =  (reader["Nombre"]).ToString(),
                                    Descripcion =  (reader["Descripcion"]).ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    CantidadDisponible = Convert.ToInt32(reader["CantidadDisponible"]),
                                };
                                 productos.Add(producto);
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
            return productos;
        }

        /// <summary>//////////// Para propiedad tipo TABLA en carritoItem (propiedad Producto)
        ///  carrito.Add(new CarritoItem 
        //        {
        //            Producto = producto,
        //            CantidadSeleccionada = 1 //Total Producto.Precio * CantidadSeleccionada;
        //        });
        //    }
        //    Session["Carrito"] = carrito; // Guardar el carrito actualizado en la sesión
        //    return RedirectToAction("Carrito"); // Redirigir a la vista del carrito
        //}        
        //public ActionResult Carrito() // Acción para mostrar el carrito
        //{            
        //    var carrito = Session["Carrito"] as List<CarritoItem>; // Obtener el carrito de la sesión
        //    return View(carrito);  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Producto ObtenerProductoEspecifico(int id) //////////// Para propiedad tipo TABLA en carritoItem (propiedad Producto)
        {
            Producto producto = null; // new Producto();
            try
            {
                using (SqlConnection con = new SqlConnection(conexion_string))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE ProductoID = @ProductoID", con))
                    {
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;// solo poner cuando SqlCommand sea un nombre de SP. OJO
                        cmd.Parameters.AddWithValue("@ProductoID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                producto = new Producto
                                {
                                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                    Nombre = (reader["Nombre"]).ToString(),
                                    Descripcion = (reader["Descripcion"]).ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    CantidadDisponible = Convert.ToInt32(reader["CantidadDisponible"]),
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
            return producto;
        }
    }
}
