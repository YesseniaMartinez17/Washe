using Inspinia_MVC5.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class LayoutsController : Controller
    {

        [Autoriza(idOperacion: 1)]
        public ActionResult Index()
        {
            return View();
        }

        [Autoriza(idOperacion: 1)]
        public ActionResult OffCanvas()
        {
            return View();
        }
	}
}