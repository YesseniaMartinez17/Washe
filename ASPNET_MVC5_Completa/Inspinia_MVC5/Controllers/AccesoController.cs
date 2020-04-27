using Inspinia_MVC5.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Usuario, string clave)
        {
            try
            {
                using (Models.WashEEntities db = new Models.WashEEntities())
                {
                    var vtbUsuarios = (from v in db.tbUsuarios
                                       where v.usu_NombreDeUsuario == Usuario && v.usu_Contrasenia == clave
                                       select v).FirstOrDefault();
                    if (vtbUsuarios == null)
                    {
                        ViewBag.Error = "Nombre de usuario o constraseña incorrecta";
                        return View();
                    }
                    Session["tbUsuarios"] = vtbUsuarios;
                }
                return RedirectToAction("Index", "Layouts");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}