using capaNegocio; //Lorena Cujilema 30 septiembre 2024
using Entidades; //Adela Cujilema 01 octubre 2024
using System; //adela lorena 1.28 pm
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaWeb1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductoBusiness _productoBusiness;

        public HomeController()
        {
            _productoBusiness = new ProductoBusiness();
        }

        public ActionResult Index()
        {
            //List<Producto> productos = new List<Producto>();
            //ViewBag.productox = _productoBusiness.ObtenerProductos(); ////// esto es para Combo

            List<Producto> listaProductos = _productoBusiness.ObtenerProductos();
            return View(listaProductos);

        }

        public ActionResult About()
        {
            ViewBag.Message = "About Lorena test viewBag. Adela Cujilema Duran";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page. Lorena";

            return View();
        }
    }
}