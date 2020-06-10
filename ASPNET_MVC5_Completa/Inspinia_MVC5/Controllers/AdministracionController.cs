using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class AdministracionController : Controller
    {
        private Helpers function = new Helpers();
        private WashEEntities db = new WashEEntities();

        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CatalogosServicios()
        {
            var tbCat = db.tbCategoriaServicios.OrderBy(x => x.cserv_Estado);
            return View(tbCat.ToList());
        }
        //[ValidateAntiForgeryToken]
        public ActionResult _Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoriaServicios tbCategoriaServicios = db.tbCategoriaServicios.Find(id);

            //tbCategoriaServicios.usu_crea = (
            //                                    from v in db.tbUsuarios
            //                                    where v.usu_Id == tbCategoriaServicios.cserv_UsuarioCrea
            //                                    select v.usu_NombreDeUsuario
            //                                ).FirstOrDefault();
            //tbCategoriaServicios.tbServicios.Count();

            //if(tbCategoriaServicios.cserv_UsuarioModifica != null)
            //{
            //    tbCategoriaServicios.usu_modifica = (
            //                                            from v in db.tbUsuarios
            //                                            where v.usu_Id == tbCategoriaServicios.cserv_UsuarioModifica
            //                                            select v.usu_NombreDeUsuario
            //                                        ).FirstOrDefault();
            //}

            var servicios = (
                                from serv in db.tbServicios
                                where serv.cserv_Id == id
                                select serv
                            ).First();

            tbCategoriaServicios.tbServicios.Add(servicios);

            if (tbCategoriaServicios == null)
            {
                return HttpNotFound();
            }
            return PartialView(tbCategoriaServicios);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult inactivar(int id, string razon)
        {
            string msj = "";
            var idUsuario = (int)Session["Id"];
            int codigo = 200;// 500;
            var list = db.UDP_Prod_tbCategoriaServicios_Inactivar(
                                                                    id
                                                                    , idUsuario
                                                                    , function.DatetimeNow()
                                                                    , razon
                                                                );
            foreach (UDP_Prod_tbCategoriaServicios_Inactivar_Result item in list)
            {
                msj = item.MensajeError + " ";
                if (Convert.ToInt32(item.MensajeError) > 0)
                {
                    codigo = 200;
                }
            }

            return new HttpStatusCodeResult(codigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult REactivar(int id)
        {
            int codigo = 500;
            string msj = "";
            int idusuario = (int)Session["Id"];

            var list = db.UDP_Prod_tbCategoriaServicios_REactivar(id, idusuario, function.DatetimeNow());

            foreach (UDP_Prod_tbCategoriaServicios_REactivar_Result item in list)
            {
                msj = item.MensajeError + " ";
                if (Convert.ToInt32(item.MensajeError) > 0)
                {
                    codigo = 200;
                }
            }

            return new HttpStatusCodeResult(codigo);
        }
    }
}