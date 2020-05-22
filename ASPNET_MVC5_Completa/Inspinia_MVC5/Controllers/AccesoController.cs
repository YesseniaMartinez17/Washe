using Inspinia_MVC5.Filters;
using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        // GET: Acceso
        public ActionResult Exit()
        {
            Session["tbUsuarios"] = null;
            return RedirectToAction("Login", "Acceso");
        }
        
        [HttpPost]
        public ActionResult Login(string Usuario, string clave)
        {
            
            var ncryptclave = Helpers.GetSHA256(clave);
            try
            {
                using (Models.WashEEntities db = new Models.WashEEntities())
                {
                    var vtbUsuarios = (from v in db.tbUsuarios
                                       where v.usu_NombreDeUsuario == Usuario && v.usu_Contrasenia == ncryptclave
                                       select v).FirstOrDefault();
                    if (vtbUsuarios == null)
                    {
                        ViewBag.Error = "Nombre de usuario o constraseña incorrecta";
                        return View();
                    }
                    Session["name"] = vtbUsuarios.usu_NombreDeUsuario.ToString();
                    Session["Id"] = vtbUsuarios.usu_Id;
                    Session["tbUsuarios"] = vtbUsuarios;
                }
                return RedirectToAction("Index", "Acceso");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}