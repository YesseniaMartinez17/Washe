using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult _Navigation()
        {

            ViewBag.Usuario = "Hola";
            return View();
        }
    }
}