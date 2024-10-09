using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaWeb1.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string PasswordHash)
        {

            return null;
        }
	}
}