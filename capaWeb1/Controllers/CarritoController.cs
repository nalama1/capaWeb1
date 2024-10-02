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
            var producto = _productoBusiness.ObtenerProductoEspecifico(codigo);
            var carrito = Session["Carrito"] as List<CarritoItem>;

            if(carrito == null)
            {
                carrito = new List<CarritoItem>();
            }

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

	}
}