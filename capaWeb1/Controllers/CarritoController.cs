using capaNegocio;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaWeb1.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ProductoBusiness _productoBusiness;
        public CarritoController()
        {
            _productoBusiness = new ProductoBusiness();
        }

        public ActionResult AgregarAlCarrito(int codigo)
        {
            //var producto = _productoBusiness.ObtenerProductoEspecifico(codigo);
            var carrito = Session["Carrito"] as List<CarritoItem>;

            if(carrito == null)
            {
                carrito = new List<CarritoItem>();
            }

            var producto = _productoBusiness.ObtenerProductoEspecifico(codigo);
            carrito.Add(new CarritoItem
            {
                Producto = producto,
                CantidadSeleccionada = 2
            });

            Session["Carrito"] = carrito;
            return RedirectToAction("Carrito");
        }

        public ActionResult Carrito()
        {
            var carrito = Session["carrito"] as List<CarritoItem>;
            if (carrito == null)
            {
                carrito = new List<CarritoItem>();
            }

            return View(carrito);
        }

        //nuevo: 2 octubre 2024
        public ActionResult RegistrarCompra()
        {
            var carrito = Session["carrito"] as List<CarritoItem>;
            if (carrito == null || !carrito.Any())
            {
                return RedirectToAction("Listado", "Producto");
            }
            //var userid = (int) Session["UserID"];

            OrdenCompraBusiness _ordenCompraBusiness = new OrdenCompraBusiness();
            bool bandera = _ordenCompraBusiness.grabarOrdenCompra(carrito, 0, "");

            if (bandera) { Console.WriteLine("Se grabó correctamente."); }

            Session["carrito"] = null;
            return View("FinalizarCompra", carrito);

        }

	}
}