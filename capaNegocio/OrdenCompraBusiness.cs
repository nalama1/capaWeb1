using capaAccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocio
{
    public class OrdenCompraBusiness
    {
        private readonly OrdenCompraRepositorio _ordenCompraRepositorio;
        public OrdenCompraBusiness()
        {
            _ordenCompraRepositorio = new OrdenCompraRepositorio();
        }

        public bool grabarOrdenCompra(List<CarritoItem> carrito, int UsuarioID, string direccionEntrega)
        {
            return _ordenCompraRepositorio.grabarOrdenCompra(carrito, UsuarioID, direccionEntrega);
        }
    }
}
