using capaAccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocio
{
    public class ProductoBusiness
    {
        private readonly ProductoRepositorio _productoRepositorio;
        public ProductoBusiness()
        {
            _productoRepositorio = new ProductoRepositorio();
        }

        public List<Producto> ObtenerProductos()
        {
            return _productoRepositorio.ObtenerProductos();
        }

        public Producto ObtenerProductoEspecifico(int id)
        {
            return _productoRepositorio.ObtenerProductoEspecifico(id);
        }


    }
}
