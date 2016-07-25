using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectF2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirecionarUsuario();
            //return View();
        }

        private ActionResult RedirecionarUsuario()
        {

            if (User.IsInRole("Usuario"))
                return RedirectToAction("Index", "Usuario");
            if (User.IsInRole("Lojista"))
                return RedirectToAction("Index", "Lojista");
            if (User.IsInRole("Administrador"))
                return RedirectToAction("Index", "Administrador");
            else
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}