using capaAccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace capaNegocio
{
    public class LocationBusiness
    {
        private readonly LocationRepositorio _locationRepositorio;

        public LocationBusiness()
        {
            _locationRepositorio = new LocationRepositorio();
        }

        public List<SelectListItem> cargarComboPaises()
        {
            return _locationRepositorio.cargarComboPaises();
        }

        public List<SelectListItem> cargarComboCiudadesXPais(int paisId)
        {
            return _locationRepositorio.cargarComboCiudadesXPais(paisId);
        }

    }
}
