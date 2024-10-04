using capaNegocio;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaWeb1.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoBusiness _productoBusiness;

        public ProductoController()
        {
            _productoBusiness = new ProductoBusiness();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listado() // Falta la VISTA, ya esta el controlador HERE LORENA........................
        {
            List<Producto> producto = _productoBusiness.ObtenerProductos();
            return View(producto);
        }



	}
}