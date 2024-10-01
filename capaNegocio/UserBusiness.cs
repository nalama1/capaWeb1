using capaAccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocio
{
    public class UserBusiness
    {
        private readonly UserRepositorio _userRepositorio;
        public UserBusiness()
        {
            _userRepositorio = new UserRepositorio();
        }

        public bool RegisterUser(Users user)
        {
            return _userRepositorio.RegisterUser(user);
        }
    }
}
